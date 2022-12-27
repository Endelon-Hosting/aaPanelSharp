// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using aaPanelSharp;
using aaPanelSharp_Test;

Console.WriteLine("Hello, World!");

aaPanel panel = new aaPanel(Creds.API_URL, Creds.API_KEY);

var phpvrs = panel.PHPVersions;

Debugger.Break();