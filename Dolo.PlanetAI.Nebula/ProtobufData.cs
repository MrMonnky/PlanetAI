using ProtoBuf;

namespace Dolo.PlanetAI.Nebula;

[ProtoContract(SkipConstructor = true)]
internal class ProtobufData
{
	[ProtoMember(1)]
	public string A { get; set; }

	[ProtoMember(2)]
	public string B { get; set; }

	[ProtoMember(4)]
	public string C { get; set; }

	[ProtoMember(5)]
	public string D { get; set; }

	[ProtoMember(6)]
	public string E { get; set; }

	[ProtoMember(8)]
	public string F { get; set; }

	[ProtoMember(14)]
	public string G { get; set; }

	[ProtoMember(16)]
	public string H { get; set; }
}
