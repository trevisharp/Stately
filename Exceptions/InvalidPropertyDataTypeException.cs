/* Author:  Leonardo Trevisan Silio
 * Date:    06/04/2023
 */
using System;

namespace Stately.Exceptions;

/// <summary>
/// This exceptions occurs when a wrong attribuition is made. 
/// </summary>
public class InvalidPropertyDataTypeException : Exception
{
    public override string Message => 
        "The property recived a invalid value for his type.";
}