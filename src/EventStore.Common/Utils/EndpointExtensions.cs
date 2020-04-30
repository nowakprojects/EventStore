using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace EventStore.Common.Utils {
	public static class EndpointExtensions {
		public static string HTTP_SCHEMA => Uri.UriSchemeHttp;
		public static string HTTPS_SCHEMA => Uri.UriSchemeHttps;

		public static string ToHttpUrl(this EndPoint endPoint, string schema, string rawUrl = null) {
			if (endPoint is IPEndPoint) {
				var ipEndPoint = endPoint as IPEndPoint;
				return CreateHttpUrl(schema, ipEndPoint.Address.ToString(), ipEndPoint.Port,
					rawUrl != null ? rawUrl.TrimStart('/') : string.Empty);
			}

			if (endPoint is DnsEndPoint) {
				var dnsEndpoint = endPoint as DnsEndPoint;
				return CreateHttpUrl(schema, dnsEndpoint.Host, dnsEndpoint.Port,
					rawUrl != null ? rawUrl.TrimStart('/') : string.Empty);
			}

			return null;
		}

		public static string ToHttpUrl(this EndPoint endPoint, string schema, string formatString,
			params object[] args) {
			if (endPoint is IPEndPoint) {
				var ipEndPoint = endPoint as IPEndPoint;
				return CreateHttpUrl(schema, ipEndPoint.Address.ToString(), ipEndPoint.Port,
					string.Format(formatString.TrimStart('/'), args));
			}

			if (endPoint is DnsEndPoint) {
				var dnsEndpoint = endPoint as DnsEndPoint;
				return CreateHttpUrl(schema, dnsEndpoint.Host, dnsEndpoint.Port,
					string.Format(formatString.TrimStart('/'), args));
			}

			return null;
		}

		public static string GetHost(this EndPoint endpoint) {
			if (endpoint is IPEndPoint ip) {
				return ip.Address.ToString();
			}

			if (endpoint is DnsEndPoint dns) {
				return dns.Host;
			}

			throw new ArgumentOutOfRangeException(nameof(endpoint), endpoint?.GetType(), "An invalid endpoint has been provided");
		}
		
		public static int GetPort(this EndPoint endpoint) {
			if (endpoint is IPEndPoint ip) {
				return ip.Port;
			}

			if (endpoint is DnsEndPoint dns) {
				return dns.Port;
			}

			throw new ArgumentOutOfRangeException(nameof(endpoint), endpoint?.GetType(), "An invalid endpoint has been provided");
		}

		public static IPEndPoint ResolveDnsToIPAddress(this EndPoint endpoint) {
			var entries = Dns.GetHostAddresses(endpoint.GetHost());
			if (entries.Length == 0)
				throw new Exception($"Unable get host addresses for DNS host ({endpoint.GetHost()})");
			var ipaddress = entries.FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork);
			if (ipaddress == null)
				throw new Exception($"Could not get an IPv4 address for host '{endpoint.GetHost()}'");
			return new IPEndPoint(ipaddress, endpoint.GetPort());
		}

		private static string CreateHttpUrl(string schema, string host, int port, string path) {
			return $"{schema}://{host}:{port}/{path}";
		}
	}
}
