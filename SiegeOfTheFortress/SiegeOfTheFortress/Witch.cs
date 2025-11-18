using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace SiegeOfTheFortress
{
    [Serializable]
    public class Witch
    {
        private bool war;
        
        public Witch(bool war)
        {
            this.war = war;
            Field.Fieldenemymove += WitchAnalize;
            Artifact.ArtifactisAttacked += StateofWar;
        }

        public void StateofWar(object sender, MyMessage mes)
        {
            war = true;
        }

        public void WitchAnalize(object sender, MyMessage mes)
        {
            if (mes.Answer != 0 && mes.Info != 0)
                mes.Answer = 2;//Наложить эффект на ближайшего

            else if (mes.Annex != 0 || war)
            {
                war = true;
                mes.Answer = 1;//Атаковать ближайшего
            }
            else
                mes.Answer = 0;//Ничего не делать, либо возвращаться к артефакту, если он не защищен
        }
    }
}
