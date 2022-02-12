using System;

namespace Dolo.PlanetAI.NET.Fluorine;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
internal class TransientAttribute : Attribute
{
}
