﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace Makaretu.Dns
{

    [TestClass]
    public class PresentationWriterTest
    {
        [TestMethod]
        public void WriteUInt16()
        {
            var text = new StringWriter();
            var writer = new PresentationWriter(text);
            writer.WriteUInt16(ushort.MaxValue);
            writer.WriteUInt16(1, appendSpace: false);
            Assert.AreEqual("65535 1", text.ToString());
        }

        [TestMethod]
        public void WriteUInt32()
        {
            var text = new StringWriter();
            var writer = new PresentationWriter(text);
            writer.WriteUInt32(int.MaxValue);
            writer.WriteUInt32(1, appendSpace: false);
            Assert.AreEqual("2147483647 1", text.ToString());
        }

        [TestMethod]
        public void WriteString()
        {
            var text = new StringWriter();
            var writer = new PresentationWriter(text);
            writer.WriteString("alpha");
            writer.WriteString("a b");
            writer.WriteString(null);
            writer.WriteString("");
            writer.WriteString(" ");
            writer.WriteString("a\\b");
            writer.WriteString("a\"b");
            writer.WriteString("end", appendSpace: false);
            Assert.AreEqual("alpha \"a b\" \"\" \"\" \" \" a\\\\b a\\\"b end", text.ToString());
        }

        [TestMethod]
        public void WriteDomainName()
        {
            var text = new StringWriter();
            var writer = new PresentationWriter(text);
            writer.WriteString("alpha.com");
            writer.WriteString("omega.com", appendSpace: false);
            Assert.AreEqual("alpha.com omega.com", text.ToString());
        }

        [TestMethod]
        public void WriteBase64String()
        {
            var text = new StringWriter();
            var writer = new PresentationWriter(text);
            writer.WriteBase64String(new byte[] { 1, 2, 3 });
            writer.WriteBase64String(new byte[] { 1, 2, 3 }, appendSpace: false);
            Assert.AreEqual("AQID AQID", text.ToString());
        }

        [TestMethod]
        public void WriteTimeSpan16()
        {
            var text = new StringWriter();
            var writer = new PresentationWriter(text);
            writer.WriteTimeSpan16(TimeSpan.FromSeconds(ushort.MaxValue));
            writer.WriteTimeSpan16(TimeSpan.Zero, appendSpace: false);
            Assert.AreEqual("65535 0", text.ToString());
        }

        [TestMethod]
        public void WriteTimeSpan32()
        {
            var text = new StringWriter();
            var writer = new PresentationWriter(text);
            writer.WriteTimeSpan32(TimeSpan.FromSeconds(int.MaxValue));
            writer.WriteTimeSpan32(TimeSpan.Zero, appendSpace: false);
            Assert.AreEqual("2147483647 0", text.ToString());
        }

        [TestMethod]
        public void WriteIPAddress()
        {
            var text = new StringWriter();
            var writer = new PresentationWriter(text);
            writer.WriteIPAddress(IPAddress.Loopback);
            writer.WriteIPAddress(IPAddress.IPv6Loopback, appendSpace: false);
            Assert.AreEqual("127.0.0.1 ::1", text.ToString());
        }

        [TestMethod]
        public void WriteDnsType()
        {
            var text = new StringWriter();
            var writer = new PresentationWriter(text);
            writer.WriteDnsType(DnsType.ANY);
            writer.WriteDnsType((DnsType)1234, appendSpace: false);
            Assert.AreEqual("ANY TYPE1234", text.ToString());
        }

        [TestMethod]
        public void WriteDnsClass()
        {
            var text = new StringWriter();
            var writer = new PresentationWriter(text);
            writer.WriteDnsClass(DnsClass.IN);
            writer.WriteDnsClass((DnsClass)1234, appendSpace: false);
            Assert.AreEqual("IN CLASS1234", text.ToString());
        }
    }
}
