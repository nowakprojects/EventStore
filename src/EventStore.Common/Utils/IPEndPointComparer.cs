using System.Collections.Generic;
using System.Net;

namespace EventStore.Common.Utils {
	public class IPEndPointComparer : IComparer<IPEndPoint> {
		public int Compare(IPEndPoint x, IPEndPoint y) {
			var xx = x.Address.ToString();
			var yy = y.Address.ToString();
			var result = string.CompareOrdinal(xx, yy);
			return result == 0 ? x.Port.CompareTo(y.Port) : result;
		}
	}
	
	public class EndPointComparer : IComparer<EndPoint> {
		public int Compare(EndPoint x, EndPoint y) {
			if (ReferenceEquals(x, y)) return 0;
			if (ReferenceEquals(x, null)) return 1; //TODO(pieterg) check this
			if (ReferenceEquals(y, null)) return -1;
			var xx = x.GetHost();
			var yy = y.GetHost();
			var result = string.CompareOrdinal(xx, yy);
			return result == 0 ? x.GetPort().CompareTo(y.GetPort()) : result;
		}
	}
}
