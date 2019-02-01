import controlP5.*;

ControlP5 cp5;
OSC myOsc = new OSC();

int sendDataWidth = 1080;
int sendDataHeight = 1080;

boolean startOSC = false;

int click=0;

void setup() {
  size(512, 512);

  cp5 = new ControlP5(this);

  // create a toggle
  cp5.addToggle("startOSC")
    .setPosition(width-100, 20)
    .setSize(50, 20)
    ;
}


void draw() {

  background(0);
  float mapDataX = map(mouseX, 0, width, 0, sendDataWidth);
  float mapDataY = map(mouseY, 0, height, sendDataHeight, 0);

  if (startOSC) {
    float[] _array = new float[2]; 
    _array[0] = mapDataX;
    _array[1] = mapDataY;
    //myOsc.oscSend_Array("/pos", _array);

    //myOsc.Sendtest("/person", mouseX, mouseY, 0);

    OscMessage myMessage = new OscMessage("/person");

    float px = map(mouseX, 0, width, 3840, -3840);
    float py = map(mouseY, 0, height, -1080, 1080);

    myOsc.Sendtest("/person", px, py, click);
  }


  if (mousePressed) {
    click=1;
  } else
    click = 0;

  ShowUI(mapDataX, mapDataY);

  if (startOSC) {
    fill(255);
    textSize(20);
    text("OSC Sending...", 0, 60);
  }
}



void ShowUI(float px, float py) {

  fill(255);
  textSize(20);
  text("OSC Tester", 5, 20);

  fill(100, 100, 255);
  ellipse(mouseX, mouseY, 5, 5);
  text(round(px)+"," + round(py), mouseX+5, mouseY-5);

  stroke(100, 100, 255);
  line(mouseX, mouseY-20, mouseX, mouseY+20); 
  line(mouseX-20, mouseY, mouseX+20, mouseY);
}

void keyPressed() {
  if (key == 'q') {
    startOSC = !startOSC;
  }
}