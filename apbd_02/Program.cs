using apbd_02;

Console.WriteLine("Hello, World!");

Device d = new Smartwatch("SW-1","Apple", true, 20);
d.Info();
d.TurnMode();
d.TurnMode();
d.TurnMode();
// d.TurnMode(); EmptyBatteryException ... < 11 %

// Device pc = new PersonalComputer(1,"Apple 2", false, null); gives an exception coz of false & null in one time
// pc.Info();
// pc.TurnMode();
// pc.Info();

Device ed = new EmbeddedDevice("ED-4", "smthg", true, "255.255.255.255", "MD Ltd. capibara");
ed.TurnMode();
ed.TurnMode();
ed.Info();

DeviceManager dm = new DeviceManager("input.txt");
dm.ShowAllDevices();
dm.EditDeviceData("P-1", new PersonalComputer("P-1","capibara",true,"lmao"));
dm.ShowAllDevices();
dm.SaveDataToFile();