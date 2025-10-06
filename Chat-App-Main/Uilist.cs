using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ChatApp
{
    internal class Uilist
    {
        // GeTTextHeight fonksiyonu, bir Label üzerindeki metnin yüksekliğini hesaplar.
        // 'Label' kontrolü ve bu kontrolün metni ile çalışır.
        public static int GeTTextHeight(Label lbl)
        {
            // 'Graphics' nesnesi, bir Label'in içeriği üzerinde çizim yapmamıza izin verir. 
            // 'lbl.CreateGraphics()' ile mevcut Label kontrolü üzerinde çizim yapabilmemiz için bir Graphics nesnesi oluşturulur.
            using (Graphics g = lbl.CreateGraphics())
            {
                // 'MeasureString' fonksiyonu, metnin belirtilen genişlikte (495 piksel) ne kadar yer kapladığını hesaplar.
                // Burada 495, etiketin genişliğini ifade eder.
                // 'lbl.Text' etiketteki metni, 'lbl.Font' etikette kullanılan yazı tipini belirtir.
                SizeF size = g.MeasureString(lbl.Text, lbl.Font, 495);

                // 'size.Height' ölçülen metnin yüksekliğini döndürür.
                // 'Math.Ceiling' fonksiyonu, ölçülen yüksekliği bir üst tamsayıya yuvarlar.
                // Bu, metnin tam olarak kaç piksel yüksekliğe ihtiyaç duyduğunu verir.
                return (int)Math.Ceiling(size.Height);
            }
        }
    }
}
