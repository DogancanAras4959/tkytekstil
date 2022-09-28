using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace tkytekstil.COMMON.Helpers
{
    public class SaveImageProcess
    {
        public static FTPInformation GetFTPInformation(string _admin)
        {
            FTPInformation ftpInfo = new FTPInformation();
            switch (_admin)
            {
                case "Admin":
                    ftpInfo.Url = "ftp://uploads.tkytekstil.com//uploads/img";
                    ftpInfo.UserName = "tkytekst_mlwh9uzjlul";
                    ftpInfo.Password = "jf5Fx27@6";
                    break;

                default:
                    break;
            }
            return ftpInfo;
        }

        public static string ImageInsert(IFormFile _file, string _admin)
        {

            try
            {

                FTPInformation fTPInformation = GetFTPInformation(_admin);
                var uploadurl = fTPInformation.Url;
                var username = fTPInformation.UserName;
                var password = fTPInformation.Password;

                string uploadfilename = Path.GetFileNameWithoutExtension(_file.FileName);
                //string extension = Path.GetExtension(_file.FileName);
                uploadfilename = uploadfilename + DateTime.Now.ToString("yymmssfff") + ".webp"; /* + extension;*/
                Stream streamObj = _file.OpenReadStream();
                byte[] buffer = new byte[_file.Length];
                streamObj.Read(buffer, 0, buffer.Length);
                streamObj.Close();
                string ftpurl = string.Format("{0}/{1}", uploadurl, uploadfilename);
                var requestObj = WebRequest.Create(ftpurl) as FtpWebRequest;
                requestObj.Method = WebRequestMethods.Ftp.UploadFile;
                requestObj.Credentials = new NetworkCredential(username, password);
                Stream requestStream = requestObj.GetRequestStream();
                requestStream.Write(buffer, 0, buffer.Length);
                requestStream.Flush();
                requestStream.Close();

                return uploadfilename;
            }
            catch (WebException ex)
            {
                String status = ((FtpWebResponse)ex.Response).StatusDescription;
                return null;
            }
        }

        public class FTPInformation
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Url { get; set; }
        }
    }
}

