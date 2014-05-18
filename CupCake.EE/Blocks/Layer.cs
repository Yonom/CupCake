using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
/// <summary>
/// Describes the layer where a block is located on.
/// </summary>
public enum Layer
{

	/// <summary>
	/// The foreground layer (contains solid, action, and decoration blocks).
	/// </summary>
	Foreground = 0,

	/// <summary>
	/// The background layer (contains background blocks).
	/// </summary>
	Background = 1

}
