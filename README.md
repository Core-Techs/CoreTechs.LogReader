CoreTechs.LogReader
===================

.NET class library for querying IIS log files with LINQ
 
### Features

 - IIS log entries are enumerable (lazy loaded)
 - Query log entries using LINQ
 - Query any/all log files at once
 - Log files locked by IIS are automatically copied to temp location and deleted after querying
  
### Installation

PM> Install-Package CoreTechs.LogReader

### Example

```C#
// linqpad example
var reader = new LogReader(@"c:\inetpub\logs");

// show available fields
reader.GetFields().Dump();

// following queries only look at past weeks log files
var logs = reader.ReadLogEntries(lf => lf.File.LastWriteTime >= DateTime.Now.Date.AddDays(-7));

// get all 5xx responses excluding 500's
(from l in logs
let status = l["sc-status"]
where status.StartsWith("5") && status != "500"
select l).Dump();

// get distinct usernames containing "ab"
(from l in logs
let username = l["cs-username"]
where username.Contains("ab")
select username).Distinct().Dump();
```
