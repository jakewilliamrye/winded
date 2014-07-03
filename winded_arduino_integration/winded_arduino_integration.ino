#define front 10
#define left 3
#define right 6
#define back 9

String msg; //protocol: {0, front, left, right, back, 255}

void setup() {
   Serial.begin(9600);  
   pinMode(front, OUTPUT);
   pinMode(left, OUTPUT);
   pinMode(right,OUTPUT);
   pinMode(back, OUTPUT);
   
}

void loop() {
    msg=getMessage();   
    if (msg.length() == 6 && msg.charAt(0) == 'a' && msg.charAt(5) == 'z'){ //check if received valid message
      analogWrite(front, map(msg.charAt(1), 'b', 'y', 0, 255));
      analogWrite(left, map(msg.charAt(2), 'b', 'y', 0, 255));
      analogWrite(right, map(msg.charAt(3), 'b', 'y', 0, 255));
      analogWrite(back, map(msg.charAt(4), 'b', 'y', 0, 255));
    } 
}

String getMessage() {
  String msg="";
  while (Serial.available() > 0) {
    msg += (char) Serial.read();
    delay(1);
  } 
  return msg; 
}
