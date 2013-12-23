// consts
int LED_PIN_01 = 6;
int LED_PIN_02 = 9;
float ledFallSpeed = 200.0f;

// this value constantly changes
float ledValue = 0.0f;

// time related
unsigned long prevFrameTime;
float dt = 0.0;

void setup()
{
  // setup LED
  pinMode(LED_PIN_01, OUTPUT);
  pinMode(LED_PIN_02, OUTPUT);

  // setup COM
  Serial.begin(9600);

  // intial value
  prevFrameTime = millis();
}

// calcs dt = time in seconds (float) from previous frame
void calcFrameTime()
{
  unsigned long currentFrameTime = millis();
  prevFrameTime = currentFrameTime - prevFrameTime;
  if(prevFrameTime < 0)
    prevFrameTime = currentFrameTime = 0;

  dt = 0.001f * prevFrameTime;
  prevFrameTime = currentFrameTime;
}

// reduces LED value over time
void calcLEDValue()
{
  if(ledValue > 0)
  {
    ledValue -= ledFallSpeed * dt;
    if(ledValue < 0)
      ledValue = 0;
  }
}

void updateLEDValue()
{
  if(Serial.available() > 0)
  {
    // read flag
    Serial.read();

    float newValue = (float)(Serial.read());
    if(newValue > ledValue)
      ledValue = newValue; 
  }
}

void setLEDValue()
{
  int pinValue = (int)(255.0 * ledValue / 100.0f);
  analogWrite(LED_PIN_01, pinValue);
  analogWrite(LED_PIN_02, pinValue);
}

void loop()
{
  updateLEDValue();

  calcFrameTime();
  calcLEDValue();

  setLEDValue();
}


