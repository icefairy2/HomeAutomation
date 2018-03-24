#include <Arduino.h>

#define THERMISTOR_NOMINAL 6800
#define B_COEFFICIENT 4200
#define TEMPERATURE_NOMINAL 25

#define SENSOR1_PIN A0
#define SERIES_RESISTANCE 10000

// setup function, called once at system initialization
void setup(){
    pinMode(SENSOR1_PIN, INPUT);
    Serial.begin(9600);
    Serial.println("Test");
}

// reads the value of a resistance connected in a voltage divider
// analogPin - number of pin the voltage divider is connected to
// seriesResistance - value of the resistance connected in series
double inline getResistanceValue(int analogPin, double seriesResistance){
    double reading = analogRead(analogPin); // get analog value [0 - 1024]
    reading = (1023 / reading) - 1; // convert to voltage
    double resistance = seriesResistance / (double)reading; // calculate resistance
    return resistance;
}

// reads the value of the resistance nrSamples times and translates the average into C degrees
// analogPin - number of pin the voltage divider is connected to
// seriesResistance - value of the resistance connected in series
// nrSamples - number of smaples to be averaged
double inline getTemperature(int analogPin, double seriesResistance, int nrSamples){
    double averageR = 0;
    for (int i = 0; i < nrSamples; i++){
        averageR += getResistanceValue(analogPin, seriesResistance);
    }
    averageR /= nrSamples; // average out the samples

    double steinhart = averageR / THERMISTOR_NOMINAL; // R/R0
    steinhart = log(steinhart); // ln(R/R0)
    steinhart /= B_COEFFICIENT; // 1/B * ln(R/R0)
    steinhart += 1.0 / (TEMPERATURE_NOMINAL + 273.15); // + (1/T0)
    steinhart = 1.0 /steinhart; // Invert
    steinhart -= 273.15; // conver to C

    return steinhart;
}

// loop function, called repeatedly
void loop(){
    float temp = getTemperature(SENSOR1_PIN, SERIES_RESISTANCE, 1);
    Serial.print("1 sample: ");
    Serial.println(temp);
    delay(100);

    temp = getTemperature(SENSOR1_PIN, SERIES_RESISTANCE, 5);
    Serial.print("5 sample: ");
    Serial.println(temp);
    delay(100);

    temp = getTemperature(SENSOR1_PIN, SERIES_RESISTANCE, 10);
    Serial.print("10 sample: ");
    Serial.println(temp);
    delay(100);

}

