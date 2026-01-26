using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Fask.MST_W
{
    public class CsvTextFileReader
    {
        public int _bufferSize;
        char[] _buffer;
        public int _position;

        private CsvTextFileReader()
        {
        }

        public CsvTextFileReader(string filename)
        {
            try
            {
                this.Read(filename);
            }
            catch
            {
            }
        }


        public bool Read(string filename)
        {
            StreamReader sr = null;
            try
            {
                FileInfo fi = new FileInfo(filename);
                _bufferSize = (int)fi.Length;
                _buffer = new char[_bufferSize];

                sr = new StreamReader(filename);
                int bytesread = sr.ReadBlock(_buffer, 0, _bufferSize);
                _position = 0;

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                    sr = null;
                }
            }
        }

        public string ReadLine()
        {

            int startpos = _position;
            int endpos = _position;
            while (_position < _bufferSize && _buffer[_position] != '\r')
                _position++;

            endpos = _position;

            if (_position < _bufferSize && _buffer[_position] == '\r')
                _position++;
            if (_position < _bufferSize && _buffer[_position] == '\n')
                _position++;

            if (endpos == startpos)
                return null;

            return (new string(_buffer, startpos, endpos - startpos));
        }
    }
}
