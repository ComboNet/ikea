using System.Diagnostics;

string logName = "DemoApp";
string sourceName = "SourceDemo";

if(!EventLog.SourceExists(sourceName, Environment.MachineName))
{
    EventLog.CreateEventSource(new EventSourceCreationData(sourceName, logName));
}