using System.Threading.Tasks;

namespace Dolo.PlanetAI.Websocket;

internal delegate Task MspEvent<in Socket, in Arg>(Socket socket, Arg arg);
