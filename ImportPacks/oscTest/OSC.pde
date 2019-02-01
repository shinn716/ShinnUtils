import oscP5.*;
import netP5.*;

class OSC {

  OscP5 oscP5;
  NetAddress myRemoteLocation;

  public int port = 12345;
  public String ip = "192.168.0.124";        //192.168.0.106      127.0.0.1

  String _address;
  float[] tvalue = new float[2];
  float offsety;

  float showpx, showpy;

  OSC() {
    oscP5 = new OscP5(this, 9999);
    myRemoteLocation = new NetAddress(ip, port);
  }

  public void Sendtest(String address, float px, float py, int click) {
    OscMessage myMessage = new OscMessage(address);
    myMessage.add(2);
    myMessage.add(px); 
    myMessage.add(py); 
    myMessage.add(click); 
    myMessage.add(0); 

    oscP5.send(myMessage, myRemoteLocation);

    _address = address;
    showpx = px;
    showpy = py;

    ShowData(new PVector(0, 60+offsety));
  }

  public void oscSend_Array(String address, float[] message) {
    OscMessage myMessage = new OscMessage(address);
    myMessage.add(message); 
    oscP5.send(myMessage, myRemoteLocation);

    _address = address;
    tvalue = message;

    ShowData(new PVector(0, 60+offsety));
  }

  public void oscSend_int(String address, int message) {
    OscMessage myMessage = new OscMessage(address);
    myMessage.add(message); 
    oscP5.send(myMessage, myRemoteLocation);
  }

  public void oscSend_float(String address, float message) {
    OscMessage myMessage = new OscMessage(address);
    myMessage.add(message); 
    oscP5.send(myMessage, myRemoteLocation);
  }

  public void ShowData(PVector pos) {
    fill(100, 255, 100);
    textSize(12);
    //text("Port: " + getPort(), pos.x, pos.y);
    text(_address + " " + round(showpx)+" " + round(showpy), pos.x, pos.y+20*1);
  }

  //void oscEvent(OscMessage theOscMessage) {
  //  /* print the address pattern and the typetag of the received OscMessage */
  //  print("### received an osc message.");
  //  print(" addrpattern: "+theOscMessage.addrPattern());
  //  println(" typetag: "+theOscMessage.typetag());
  //  println(" value: " + theOscMessage.get(0).stringValue());
  //}
}
