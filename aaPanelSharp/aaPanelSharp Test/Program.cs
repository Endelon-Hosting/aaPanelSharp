// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using aaPanelSharp;
using aaPanelSharp_Test;

Console.WriteLine("Hello, World!");

aaPanel panel = new aaPanel(Creds.API_URL, Creds.API_KEY);
var s1 = panel.CreateDatabase("a", "a", "a");
var dbs = panel.Databases;
var s2 = dbs[0].Delete();

Debugger.Break();