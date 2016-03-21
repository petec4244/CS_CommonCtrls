using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace CommonCtrls
{
  ///<summary>
  ///This class is designed to read or create an XML file for things like 
  ///           AutoTextComplete, ListBox Items, Log Creation Etc..
  ///</summary>
  public class XMLHandler
  {
    //File to read if exists
    public string HistoryFile = "";
    public XMLData X_Data = new XMLData();
    
    ///<summary>
    ///Public data structure to hold the contents of the read XML file. 
    ///</summary>
    public class XMLData
    {
      public string HistoryFile_D { get; set; }
      
      ///add any number of lists to collect here.      
      public List<string> exampleList = new List<string>();
      public List<string> exampleList2 = new List<string>();
      
      public void ClearLists()
      {
        exampleList.Clear();
      }
    }
    
    ///<summary>
    ///Reads a five XMLFile and appends the appropriate list with the results
    ///</summary>
    public void ReadFile(string ReadFile)
    {
      if(File.Exists(ReadFile)
      {
        X_Data.ClearLists();
        XmlDocument xml = new XmlDocument();
        try
        {
          xml.Load(ReadFile);
          string tempStr = "";
          
          //Any number of expected nodes to find.
          string[] NodeNames = {"ExampleNode1", "ExampleNode2"};
          
          foreach(string SelectNode in NodeNames)
          {
            XmlNodeList xnList = xml.SelectNodes("/Data/DataType/"+SelectNode);
            foreach(XmlNode xn in xnList)
            {
              tempStr = "";
              tempStr = xn.InnerText;
              
              switch(SelectNode)
              {
                case "GenInfo": //this is just logged information for user info only, not used elsewhere
                  break;
                case "ExampleNode1":
                  if(!X_Data.exampleList.Contains(tempStr))
                  {
                    X_Data.exampleList.Add(tempStr);
                  }
                  break;
                case "ExampleNode2":
                  if(!X_Data.exampleList2.Contains(tempStr))
                  {
                    X_Data.exampleList2.Add(tempStr);
                  }
                  break;
              }
            }
           catch(Exception e)
           {
              System.Windows.Form.MessageBox.Show("Error Reading XML File: "  + e.ToString());
           }
          }
        }
      }
    }
    
    ///<summary>
    ///Reads the file and appends it if the newDatadata is truly new. 
    ///</summary>
    public void WriteAppend(string writeFile, EntryType Type, string newData)
    {
      string toWrite = "";
      
	  //first make certain the path exists, then the file
	  //create either if not. 
      string writepath = Path.GetDirectoryName(writeFile);
	  
	  if(!Directory.Exists(writepath)
	  {
		Directory.CreateDirectory(writepath);
	  }
	  if(!File.Exists(writeFile))
	  {
		toWrite = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n\<Data>\n\t<DataType>"+"\n\t\t<"+
				  Type.ToString() + ">" + newData + "</" + Type.ToString()+
				  ">\n\t</DataType>\n</Data>";
		File.AppendAllText(writeFile, toWrite);
		
		else
		{
			bool EntryExists = CheckList(CheckTypeRetList(Type), newData);
			if(!EntryExists)
			{
				var lines = File.ReadAllLines(writeFile);
				File.WriteAllLines(writeFile, lines.Take(lines.Length -1));
				toWrite = "\t<DataType>\n\t\t<" + Type.ToString() + ">"+newData+"</"+Type.ToString()+
				">\n\t</DataType>\n</Data>";
				File.AppendAllText(writeFile, toWrite);
			}
		}
		ReadFile(writeFile);
	  }      
    }
	
	///Determine if an entry is already in a list.
	public bool CheckList(List<string> Check, string newEntry)
	{
		bool inList = false;
		foreach(string check_s in Check)
		{
			if(check_s == newEntry)
			{
				inList=true;
				break;
			}
		}
		return inList;
	}
 
  ///<summary>
  ///Define the types to use in the switch case. 
  ///</summary>
  public enum EntryType : byte
  {
	ExampleNode1, ExampleNode2
  };
 
  }
}
