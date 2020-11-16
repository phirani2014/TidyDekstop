using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TidyDesktop
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder nofile = new StringBuilder();
            DirectoryInfo d = new DirectoryInfo(@"C:\Users\"+ Environment.UserName + @"\Desktop");
            FileInfo[] Files = d.GetFiles();
            string[] mykey = Properties.Settings.Default.ExcludeItems.Split(',');
            if (Files.Length != 0)
            {
                foreach (FileInfo file in Files)
                
                {
                    if (!mykey.Contains(file.Extension))
                    {

                        string str = file.Directory + @"\" + file.Extension + "Files";
                        bool exists = System.IO.Directory.Exists(@str);
                        string source = @file.FullName;

                        string destination = @file.DirectoryName + @"\" + file.Extension + "Files" + @"\" + file.Name;
                        if (!exists)
                            try
                            {

                                Directory.CreateDirectory(str);
                                exists = true;

                            }
                            catch (Exception ex)
                            {
                                sb.Append(ex.Message.ToString());
                                System.IO.File.WriteAllText(d.ToString() + @"\" + "Error.txt", sb.ToString());

                            }
                        if (exists)
                        {
                            bool FileExists = System.IO.File.Exists(@destination);
                            if (!FileExists)
                            {
                                try
                                {
                                    System.IO.File.Move(@source, @destination);
                                }
                                catch (Exception ex)
                                {
                                    sb.Append(ex.Message.ToString());
                                    System.IO.File.WriteAllText(d.ToString() + @"\" + "Error.txt", sb.ToString());
                                }
                            }

                            if (FileExists)
                            {

                                string dir = str + @"\" + DateTime.Now.ToString("ddMMyyyy-HHmmss");

                                try
                                {
                                    Directory.CreateDirectory(dir);
                                    destination = dir + @"\" + file.Name;
                                    System.IO.File.Move(@source, @destination);
                                }
                                catch (Exception ex)
                                {
                                    sb.Append(ex.Message.ToString());
                                    System.IO.File.WriteAllText(d.ToString() + @"\" + "Error.txt", sb.ToString());
                                }

                            }
                        }
                       
                    }

                }
                if (Properties.Settings.Default.OneFolder == "Y")
                {
                    string fdestination = @"C:\Users\"+ Environment.UserName + @"\Desktop\Sorted Desktop\"+ DateTime.Now.ToString("ddMMyyyy-HHmmss");
                    
                    DirectoryInfo[] SortedFiles = d.GetDirectories();
                    Directory.CreateDirectory(@fdestination);
                    foreach (var dir in SortedFiles)
                    {
                        if (dir.Name.Contains("Files"))
                        {
                            System.IO.Directory.Move(dir.FullName, fdestination + @"\" + dir.Name);
                        }
                      
                    }
                    
                }
            }
            else
            {
                nofile.Append("No files to transfer");
                System.IO.File.WriteAllText(d.ToString() + @"\" + "Message.txt", nofile.ToString());
            }
            
        }

        
    }
}
