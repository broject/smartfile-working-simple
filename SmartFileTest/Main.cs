using System;
using System.IO;
using System.Web;
using System.Net;
using System.Collections;

using SmartFile;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;

namespace SmartFileTest
{
    class MainClass
    {
        //smartfile.com file sharing service
        public static int Main(string[] args)
        {
            //1.make dir

            BasicClient api = new BasicClient(key, pass);
            Hashtable p = new Hashtable();
            p.Add("path", "newdir");
            WebResponse r = api.Post("/path/oper/mkdir/", null, p);

            Stream s = r.GetResponseStream();
            byte[] buff = new byte[4096];
            int count = s.Read(buff, 0, buff.Length);
            SmartObj obj = GetSmartObj(buff, count);
            string dirname = obj.name;
            s.Close();

            //1.upload

            BasicClient api_upload = new BasicClient(key, pass);
            Hashtable p_upload = new Hashtable();
            p_upload.Add("file", new FileInfo(@"D:\Temp\new stanford.jpg"));
            WebResponse r_upload = api_upload.Post("/path/data/" + dirname + "/", null, p_upload);

            Stream s_upload = r_upload.GetResponseStream();
            byte[] buff_upload = new byte[4096];
            int count_upload = s_upload.Read(buff_upload, 0, buff_upload.Length);
            SmartObj obj_upload = GetSmartObjList(buff_upload, count_upload)[0];
            s_upload.Close();

            //2.download

            BasicClient api_download = new BasicClient(key, pass);
            WebResponse r_download = api_download.Get("/path/data" + obj_upload.path);
            Stream s_download = r_download.GetResponseStream();
            using (FileStream file = new FileStream("E:\\new stanford.jpg", FileMode.Create, FileAccess.Write))
            {
                byte[] buff_download = new byte[4096 * 2];
                int count_download = s_download.Read(buff_download, 0, buff_download.Length);
                while (count_download > 0)
                {
                    file.Write(buff_download, 0, count_download);
                    count_download = s_download.Read(buff_download, 0, buff_download.Length);
                }
            }
            s_download.Close();

            return 0;
        }


        /*boroo: json data convert to model class*/

        public static List<SmartObj> GetSmartObjList(byte[] bytes, int bytesRead)
        {
            List<SmartObj> objs = new List<SmartObj>();
            try
            {
                MemoryStream ms = new MemoryStream(bytes, 0, bytesRead);
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(objs.GetType());
                objs = (List<SmartObj>)serializer.ReadObject(ms);
                ms.Close();
            }
            catch (Exception ex)
            {
                ex.ToString();
                return null;
            }
            return objs;
        }

        public static SmartObj GetSmartObj(byte[] bytes, int bytesRead)
        {
            SmartObj obj = new SmartObj();
            try
            {
                MemoryStream ms = new MemoryStream(bytes, 0, bytesRead);
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                obj = (SmartObj)serializer.ReadObject(ms);
                ms.Close();
            }
            catch (Exception ex)
            {
                ex.ToString();
                return null;
            }
            return obj;
        }
    }


}
