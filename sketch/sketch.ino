#define LED_PIN 6
#define CONTROL_PIN 7
#define SERIAL_SPEED 9600
#define SHIFT 9
#define DECREMENT 1

///////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////

int value = 0;

////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////

void updateValue()
{
    if(value > 0)
    {
        value -= DECREMENT;
    }
}

void updateLED()
{
    analogWrite(LED_PIN, value >> SHIFT);
}

///////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////

void serialEvent()
{
    value = Serial.read();
    digitalWrite(CONTROL_PIN, (value & 1) ? HIGH : LOW);
    value <<= SHIFT;
}

void setup()
{
    pinMode(LED_PIN, OUTPUT);
    pinMode(CONTROL_PIN, OUTPUT);
    Serial.begin(SERIAL_SPEED);
}

void loop()
{
    updateValue();
    updateLED();
}

