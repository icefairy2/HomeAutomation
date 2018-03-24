#include <Arduino.h>

#define THERMISTOR_NOMINAL 6800 // nominal resistance value of the thermistor at nominal temperature
#define B_COEFFICIENT 4200 // B coefficient of the thermistor
#define TEMPERATURE_NOMINAL 25 // nominal temperature of the thermistor

#define SENSOR_TEMPERATURE_INDOOR A0 // pin of the indoor temperature sensor
#define SENSOR_TEMPERATURE_OUTDOOR A1 // pin of the outdoor temperature sensor
#define SERIES_RESISTANCE 10000 // value of the series resistance for the voltage divider

#define SAMPLES 10 // number of samples for analog read

enum RequestType
{
    error_request = -1,
    temperature_indoor_request = 0,
    temperature_outdoor_request = 1,
	lamp_on_request = 2,
	lamp_off_request = 3,
};

// accepted request strings
const String requestMessage[] = {
    "TEMPERATURE_INDOOR",
    "TEMPERATURE_OUTDOOR",
	"LAMP_ON",
	"LAMP_OFF",
    "LAMP_STATUS",
    "WINDOW_STATUS",
    "HEAT_ON",
    "HEAT_OFF",
};

// reads the value of the resistance nrSamples times and translates the average into C degrees
// analogPin - number of pin the voltage divider is connected to
// seriesResistance - value of the resistance connected in series
// nrSamples - number of samples to be averaged
double inline getTemperature(int analogPin, double seriesResistance, int nrSamples);

// reads the value of a resistance connected in a voltage divider
// analogPin - number of pin the voltage divider is connected to
// seriesResistance - value of the resistance connected in series
double inline getResistanceValue(int analogPin, double seriesResistance);

// creates the response string given the type of the request
// requestType - type of received request
String createResponse(RequestType requestType);

// setup function, called once at system initialization
void setup()
{
    // set pin mode of temperature sensor
    pinMode(SENSOR_TEMPERATURE_INDOOR, INPUT);
    pinMode(SENSOR_TEMPERATURE_OUTDOOR, INPUT);
    // initialize serial communication
    Serial.begin(9600);
}

double userTemperature = 25.0;

// loop function, called repeatedly
void loop()
{

}

// processes the request from Serial
void serialEvent()
{

    String request = "";

    while (Serial.available())
    {
        delay(3); //delay to allow buffer to fill
        if (Serial.available() > 0)
        {
            char c = Serial.read(); //gets one byte from serial buffer
            request += c;           //makes the string readString
        }
    }

    RequestType requestType = error_request;

    if (request == requestMessage[temperature_indoor_request])
    {
        requestType = temperature_indoor_request;
    }

    if (request == requestMessage[temperature_outdoor_request])
    {
        requestType = temperature_outdoor_request;
    }

    String response = createResponse(requestType);

    Serial.println(response);
}

double inline getResistanceValue(int analogPin, double seriesResistance)
{
    double reading = analogRead(analogPin);                 // get analog value [0 - 1024]
    reading = (1023 / reading) - 1;                         // convert to voltage
    double resistance = seriesResistance / (double)reading; // calculate resistance
    return resistance;
}

double inline getTemperature(int analogPin, double seriesResistance, int nrSamples)
{
    double averageR = 0;
    for (int i = 0; i < nrSamples; i++)
    {
        averageR += getResistanceValue(analogPin, seriesResistance);
    }
    averageR /= nrSamples; // average out the samples

    double steinhart = averageR / THERMISTOR_NOMINAL;  // R/R0
    steinhart = log(steinhart);                        // ln(R/R0)
    steinhart /= B_COEFFICIENT;                        // 1/B * ln(R/R0)
    steinhart += 1.0 / (TEMPERATURE_NOMINAL + 273.15); // + (1/T0)
    steinhart = 1.0 / steinhart;                       // Invert
    steinhart -= 273.15;                               // conver to C

    return steinhart;
}

String createResponse(RequestType requestType){
    String response = "";

    switch(requestType){
        case temperature_indoor_request:
            response += getTemperature(SENSOR_TEMPERATURE_INDOOR, SERIES_RESISTANCE, SAMPLES);
            break;
        case temperature_outdoor_request:
            response += getTemperature(SENSOR_TEMPERATURE_OUTDOOR, SERIES_RESISTANCE, SAMPLES);
            break;
        default:
            response += "ERROR";
            break;
    }

    return response;
}