using System.Net;
using EventStore.Common.Utils;

namespace EventStore.Core.Data {
	public class GossipAdvertiseInfo {
		public EndPoint InternalTcp { get; set; }
		public EndPoint InternalSecureTcp { get; set; }
		public EndPoint ExternalTcp { get; set; }
		public EndPoint ExternalSecureTcp { get; set; }
		public EndPoint InternalHttp { get; set; }
		public EndPoint ExternalHttp { get; set; }
		public string AdvertiseInternalHostAs { get; set; }
		public string AdvertiseExternalHostAs { get; set; }
		public int AdvertiseInternalHttpPortAs { get; set; }
		public int AdvertiseExternalHttpPortAs { get; set; }

		public GossipAdvertiseInfo(EndPoint internalTcp, EndPoint internalSecureTcp,
			EndPoint externalTcp, EndPoint externalSecureTcp,
			EndPoint internalHttp, EndPoint externalHttp,
			string advertiseInternalHostAs, string advertiseExternalHostAs,
			int advertiseInternalHttpPortAs, int advertiseExternalHttpPortAs) {
			Ensure.Equal(false, internalTcp == null && internalSecureTcp == null, "Both internal TCP endpoints are null");

			InternalTcp = internalTcp;
			InternalSecureTcp = internalSecureTcp;
			ExternalTcp = externalTcp;
			ExternalSecureTcp = externalSecureTcp;
			InternalHttp = internalHttp;
			ExternalHttp = externalHttp;
			AdvertiseInternalHostAs = advertiseInternalHostAs;
			AdvertiseExternalHostAs = advertiseExternalHostAs;
			AdvertiseInternalHttpPortAs = advertiseInternalHttpPortAs;
			AdvertiseExternalHttpPortAs = advertiseExternalHttpPortAs;
		}

		public override string ToString() {
			return string.Format(
				"IntTcp: {0}, IntSecureTcp: {1}\nExtTcp: {2}, ExtSecureTcp: {3}\nIntHttp: {4}, ExtHttp: {5}, IntAdvertiseAs: {6}:{7}, ExtAdvertiseAs: {8}:{9}",
				InternalTcp, InternalSecureTcp, ExternalTcp, ExternalSecureTcp, InternalHttp, ExternalHttp,
				AdvertiseInternalHostAs, AdvertiseInternalHttpPortAs, AdvertiseExternalHostAs, AdvertiseExternalHttpPortAs);
		}
	}
}
