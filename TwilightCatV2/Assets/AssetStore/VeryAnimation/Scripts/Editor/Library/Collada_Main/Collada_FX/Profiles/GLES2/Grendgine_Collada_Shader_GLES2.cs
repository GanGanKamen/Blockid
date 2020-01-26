using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
namespace VeryAnimation.grendgine_collada
{
	[System.SerializableAttribute()]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="shader", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Grendgine_Collada_Shader_GLES2 : Grendgine_Collada_Shader
	{

	    [XmlElement(ElementName = "compiler")]
		public Grendgine_Collada_Compiler[] Compiler;			
	    [XmlElement(ElementName = "extra")]
		public Grendgine_Collada_Extra[] Extra;	
	}
}

