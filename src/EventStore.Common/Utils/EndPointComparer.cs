using System;
using System.Collections.Generic;
using System.Net;

namespace EventStore.Common.Utils {
	public class EndPointComparer : IComparer<EndPoint> {
		public int Compare(EndPoint x, EndPoint y) {
			var xx = x.GetHost();
			var yy = y.GetHost();
			var result = string.CompareOrdinal(xx, yy);
			return result == 0 ? x.GetPort().CompareTo(y.GetPort()) : result;
		}
	}
	
	public class EndPointEqualityComparer : IEqualityComparer<EndPoint> {
		public bool Equals(EndPoint x, EndPoint y) {
			return x.GetHost().Equals(y.GetHost()) && x.GetPort() == y.GetPort();
		}

		public int GetHashCode(EndPoint obj) {
            return obj.GetHost().GetHashCode() ^ obj.GetPort();
		}
	}
}
