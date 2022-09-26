String btnState(uint8_t btnState){
  if(btnState == HIGH){
    return "OFF";
  }
  else return "ON";
}

void task()
{
  enum class TaskStates
  {
    INIT,
    WAIT_COMMANDS
  };
  
    static TaskStates taskState = TaskStates::INIT;
    constexpr uint8_t button1Pin = 13;
    constexpr uint8_t button2Pin = 12;
    constexpr uint8_t button3Pin = 32;
    constexpr u_int8_t led_rojo = 14;
    constexpr u_int8_t led_amarillo = 27;
    constexpr u_int8_t led_verde = 26;

  switch (taskState)
  {
  case TaskStates::INIT:
  {
    Serial.begin(115200);
    pinMode(button1Pin, INPUT_PULLUP);
    pinMode(button2Pin, INPUT_PULLUP);
    pinMode(button3Pin, INPUT_PULLUP);
    pinMode(led_rojo, OUTPUT);
    digitalWrite(led_rojo, LOW);
    pinMode(led_amarillo, OUTPUT);
    digitalWrite(led_amarillo, LOW);
    pinMode(led_verde, OUTPUT);
    digitalWrite(led_verde, LOW);

    taskState = TaskStates::WAIT_COMMANDS;
    
    break;
  }

  case TaskStates::WAIT_COMMANDS:
  {
    if (Serial.available() > 0)
    {
      String command = Serial.readStringUntil('\n');
      if (command == "Led1ON")
      {
        digitalWrite(led_rojo, HIGH);
      }
      else if (command == "Led1OFF")
      {
        digitalWrite(led_rojo, LOW);
      }
      else if (command == "Led2ON")
      {
        digitalWrite(led_amarillo, HIGH);
      }
      else if (command == "Led2OFF")
      {
        digitalWrite(led_amarillo, LOW);
      }
      else if (command == "Led3ON")
      {
        digitalWrite(led_verde, HIGH);
      }
      else if (command == "Led3OFF")
      {
        digitalWrite(led_verde, LOW);
      }
      
      if (command == "readBUTTONS")
        {

        Serial.print(btnState(digitalRead(button1Pin)).c_str());
        Serial.print(btnState(digitalRead(button2Pin)).c_str());
        Serial.print(btnState(digitalRead(button3Pin)).c_str());
        Serial.print('\n');

        }
    }
    break;
  }
  default:
  {
    break;
  }
  }
  
}
void setup()
{
  task();
}

void loop()
{
  task();
}
