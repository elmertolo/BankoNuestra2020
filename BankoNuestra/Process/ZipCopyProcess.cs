﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;


namespace BankoNuestra.Process
{
    class ZipCopyProcess
    {
        
        public void CreateZipFile(string _sourcePath, string _destinationPath)
        {

             ZipFile.CreateFromDirectory(_sourcePath, _destinationPath);
        }
        public void ExtractZipFile(string sourcePath, string destinationPath)
        {

            ZipFile.ExtractToDirectory(sourcePath, destinationPath);
        }

        public void ZipFileS(string _processby, frmMain main)
        {

            string sPath = Application.StartupPath + "\\Output\\"+ frmMain._outputfolder;
            string dPath = "K:\\Zips\\Banko_Nuestra\\Test\\AFT_" + main.batchFile + "_" + _processby + ".zip";
            DeleteZipfile();
            ZipFile.CreateFromDirectory(sPath, dPath);
            
            CopyZipFile(_processby,main);
            DeleteSQl();
        }
        public void ZipFileS2(string _processby, Main2 main)
        {

            string sPath = Application.StartupPath + "\\Output\\" + Main2.outputFolder;
            string dPath = Application.StartupPath+ "\\Output\\AFT_" + main.batchfile + "_" + _processby + ".zip";
            DeleteZipfile();
            ZipFile.CreateFromDirectory(sPath, dPath);
            Ionic.Zip.ZipFile zips = new Ionic.Zip.ZipFile(dPath);
            zips.AddItem(Application.StartupPath + "\\Head");
            zips.Save();
            // CopyZipFile2(_processby, main);
            DeleteFiles(".SQL");
        }
        public void DeleteZipfile()
        {

            DirectoryInfo di = new DirectoryInfo(Application.StartupPath +"\\Output");
            FileInfo[] files = di.GetFiles("*.zip")
                     .Where(p => p.Extension == ".zip").ToArray();
            foreach (FileInfo file in files)
            {
                file.Attributes = FileAttributes.Normal;
                File.Delete(file.FullName);
            }
        }
        public static void DeleteTxtFile()
        {

            DirectoryInfo di = new DirectoryInfo(Application.StartupPath + "\\Output\\Regular_Checks");
            FileInfo[] files = di.GetFiles("*.txt")
                     .Where(p => p.Extension == ".txt").ToArray();
            foreach (FileInfo file in files)
            {
                file.Attributes = FileAttributes.Normal;
                File.Delete(file.FullName);
            }
        }
        public void CopyZipFile(string _processby,frmMain main)
        {
            string sPath = Application.StartupPath + @"\Output\AFT_" + main.batchFile + "_" + _processby + ".zip";
            string dPath = "\\\\192.168.0.254\\captive\\Zips\\Banko_Nuestra\\Test\\AFT_" + main.batchFile + "_" + _processby + ".zip";
            File.Copy(sPath, dPath, true);
            //string dPath2 = "\\\\192.168.0.254\\PrinterFiles\\ISLA\\2019\\";
            //string sPath2 = "\\\\192.168.0.254\\captive\\Auto\\IslaBank\\Test\\";

        }
        public void CopyZipFile2(string _processby, Main2 main)
        {
            string dPath = Application.StartupPath + @"\AFT_" + main.batchfile + "_" + _processby + ".zip";
            string sPath = "\\\\192.168.0.254\\captive\\Zips\\Banko_Nuestra\\Test\\AFT_" + main.batchfile + "_" + _processby + ".zip";
            File.Copy(sPath, dPath, true);
            //string dPath2 = "\\\\192.168.0.254\\PrinterFiles\\ISLA\\2019\\";
            //string sPath2 = "\\\\192.168.0.254\\captive\\Auto\\IslaBank\\Test\\";

        }

        public static void CopyPrinterFile(string _checkType, frmMain _mainForm)
        {
            string source = Application.StartupPath + "\\Output";
            string destination = @"\\192.168.0.254\PrinterFiles\Banko_Nuestra\Test\" + DateTime.Now.Year;


            if (_checkType == "A")
              File.Copy(source + "\\Regular_Checks\\BNU" + _mainForm.batchFile.Substring(0, 4) + "P.txt", destination + "\\BNU" + _mainForm.batchFile.Substring(0, 4) + "P.txt", true);
            else if ( _checkType == "B")
              File.Copy(source + "\\Regular_Checks\\BNU" + _mainForm.batchFile.Substring(0, 4) + "C.txt", destination + "\\BNU" + _mainForm.batchFile.Substring(0, 4) + "C.txt", true);
            else if ( _checkType == "CS")
                File.Copy(source + "\\Charge_Slip\\BNU" + _mainForm.batchFile.Substring(0, 4) + "CS.txt", destination + "\\BNU" + _mainForm.batchFile.Substring(0, 4) + "CS.txt", true);
        }
        public static void CopyPrinterFile2(string _checkType, Main2 _mainForm)
        {
            string source = Application.StartupPath + "\\Output";
            string destination = @"\\192.168.0.254\PrinterFiles\Banko_Nuestra\Test\" + DateTime.Now.Year;


            if (_checkType == "A")
                File.Copy(source + "\\Regular_Checks\\BNU" + _mainForm.batchfile.Substring(0, 4) + "P.txt", destination + "\\BNU" + _mainForm.batchfile.Substring(0, 4) + "P.txt", true);
            else if (_checkType == "B")
                File.Copy(source + "\\Regular_Checks\\BNU" + _mainForm.batchfile.Substring(0, 4) + "C.txt", destination + "\\BNU" + _mainForm.batchfile.Substring(0, 4) + "C.txt", true);
            //else if (_checkType == "CS")
            //    File.Copy(source + "\\Charge_Slip\\BNU" + _mainForm.batchfile.Substring(0, 4) + "CS.txt", destination + "\\BNU" + _mainForm.batchfile.Substring(0, 4) + "CS.txt", true);
        }
        public static void CopyPackingDBF(string _checkType, frmMain _mainForm)
        {
            string source = Application.StartupPath + "\\Output";
            string destination = @"\\192.168.0.254\Packing\Banko_Nuestra\TEST\";
            {
                Directory.CreateDirectory(destination + _mainForm.batchFile);

            }
           
            string destination1 = @"\\192.168.0.254\Packing\Banko_Nuestra\TEST\" + _mainForm.batchFile;
        
                    
            if (_checkType == "A" ||  _checkType == "B")
                File.Copy(source + "\\Regular_Checks\\Packing.dbf", destination1 +  "\\Packing.dbf", true);
           
            else if (_checkType == "CS")
                File.Copy(source + "\\Charge_Slip\\Packing.dbf", destination1 + "\\Packing.dbf" , true);
        }
        public static void CopyPackingDBF2(string _checkType, Main2 _mainForm)
        {
            string source = Application.StartupPath + "\\Output";
            string destination = @"\\192.168.0.254\Packing\Banko_Nuestra\TEST\";
            {
                Directory.CreateDirectory(destination + _mainForm.batchfile);

            }

            string destination1 = @"\\192.168.0.254\Packing\Banko_Nuestra\TEST\" + _mainForm.batchfile;


            if (_checkType == "A" || _checkType == "B")
                File.Copy(source + "\\Regular_Checks\\Packing.dbf", destination1 + "\\Packing.dbf", true);

            //else if (_checkType == "CS")
            //    File.Copy(source + "\\Charge_Slip\\Packing.dbf", destination1 + "\\Packing.dbf", true);
        }
        public void DeleteSQl()
        {

            DirectoryInfo di = new DirectoryInfo(Application.StartupPath +  @"\Output\" + frmMain._outputfolder);
            FileInfo[] files = di.GetFiles("*.SQL")
                     .Where(p => p.Extension == ".SQL").ToArray();
            foreach (FileInfo file in files)
            {
                file.Attributes = FileAttributes.Normal;
                File.Delete(file.FullName);
            }
        }
        public void DeleteFiles(string _ext)
        {

            DirectoryInfo di = new DirectoryInfo(Application.StartupPath + @"\Output\" + Main2.outputFolder);
            FileInfo[] files = di.GetFiles("*"+_ext)
                     .Where(p => p.Extension == _ext).ToArray();
            foreach (FileInfo file in files)
            {
                file.Attributes = FileAttributes.Normal;
                File.Delete(file.FullName);
            }
        }
        public void DeleteHead(string _ext)
        {

            DirectoryInfo di = new DirectoryInfo(Application.StartupPath + @"\Head");
            FileInfo[] files = di.GetFiles("*" + _ext)
                     .Where(p => p.Extension == _ext).ToArray();
            foreach (FileInfo file in files)
            {
                file.Attributes = FileAttributes.Normal;
                File.Delete(file.FullName);
            }
        }
    }
}
