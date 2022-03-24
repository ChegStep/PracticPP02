using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ZadaniePM01
{
    class Manager
    {
        private static PracticPP02Entities _context;
        public static Frame MainFrame { get; set; }
        public static PracticPP02Entities GetContext()
        {
            if (_context == null)
            {
                _context = new PracticPP02Entities();
            }
            return _context;
        }
    }
}
