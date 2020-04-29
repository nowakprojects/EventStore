using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace EventStore.Transport.Tcp {
	public interface ITcpConnection {
		event Action<ITcpConnection, SocketError> ConnectionClosed;

		Guid ConnectionId { get; }
		string ClientConnectionName { get; }
		EndPoint RemoteEndPoint { get; }
		EndPoint LocalEndPoint { get; }
		int SendQueueSize { get; }
		int PendingSendBytes { get; }
		bool IsClosed { get; }

		void ReceiveAsync(Action<ITcpConnection, IEnumerable<ArraySegment<byte>>> callback);
		void EnqueueSend(IEnumerable<ArraySegment<byte>> data);
		void Close(string reason);
		void SetClientConnectionName(string clientConnectionName);
	}
}
