using System;
using System.Xml;

namespace Xevle.Core
{
	public static class XmlDocumentExtensions
	{
		public static XmlNode AddRoot(this XmlDocument document, string name)
		{
			return document.AppendChild(document.CreateElement(name));
		}

		public static void AddAttribute(this XmlDocument document, XmlNode nodeToAdd, string attributeName, int attributeValue)
		{
			AddAttribute(document, nodeToAdd, attributeName, attributeValue.ToString());
		}

		public static void AddAttribute(this XmlDocument document, XmlNode addNode, string attributeName, string attributeValue)
		{
			XmlDocument TmpXmlDoc=addNode.OwnerDocument;
			XmlAttribute AddAttribute=TmpXmlDoc.CreateAttribute(attributeName);
			AddAttribute.Value=attributeValue;
			addNode.Attributes.SetNamedItem(AddAttribute);
		}

		public static XmlNode AddElement(this XmlDocument document, XmlNode node, string name, string content)
		{
			XmlNode AddNode=document.CreateElement(name);
			AddNode.InnerText=content;
			return node.AppendChild(AddNode);
		}

		public static XmlNode AddElement(this XmlDocument document, XmlNode node, string name)
		{
			XmlNode AddNode=document.CreateElement(name);
			return node.AppendChild(AddNode);
		}
	}
}

