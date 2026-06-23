using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace TestWpfReadExpFile1.Model;

#pragma warning disable format
public class ACID_SUBS { public ACID ACID { get; set; } }
public class AFOAM_SUBS { public AFOAM AFOAM { get; set; } }
public class AIR_SUBS { public AIR AIR { get; set; } }
public class BASE_SUBS { public BASE BASE { get; set; } }
public class BLUEVARY_SUB { public BLUEVARY BLUEVARY { get; set; } }
public class CGQBIOR_SUBS { public CGQBIOR CGQBIOR { get; set; } }
public class CO2PROBE_SUBS { public CO2_PROBE CO2_PROBE { get; set;} }
public class CO2_SUBS { public CO2 CO2 { get; set; } }
public class DO_SUBS { public DO DO { get; set; } = new(); }
public class ECPROBE_SUB { public EC_PROBE ECPROBE { get; set;} }
public class EXHAUST_SUBS { public EXHAUST EXHAUST { get; set; } = new(); }
public class EXPERIMENT_SUBS { public EXPERIMENT EXPERIMENT { get; set; } }
public class FOAMLIMIT_SUBS { public FOAM_LIMIT FOAMLIMIT { get; set; } }
public class GASMIXER_SUBS { public GAS_MIXER GASMIXER { get; set;} }
public class LEVEL_SUBS { public LEVEL LEVEL { get; set; } = new(); }
public class LEVELLIMIT_SUBS { public LEVEL_LIMIT LEVELLIMIT { get; set; } = new(); }
public class LIGHT_SUBS { public LIGHT LIGHT { get; set; } }
public class WEIGHT_SUBS { public WEIGHT WEIGHT { get; set; } }
public class N2_SUBS { public N2 N2 { get; set; } }
public class O2_SUBS { public O2 O2 { get; set; } }
public class PH_SUBS { public PH PH { get; set; } }
public class STIRRER_SUBS { public STIRRER STIRRER { get; set; } = new(); }
public class SUBA_SUBS { public SUBA SUBA { get; set; } }
public class SYSTEM_SUBS { public SYSTEM SYSTEM { get; set; } }
public class TEMP_SUBS { public TEMP TEMP { get; set; } = new(); }
public class TURBIDITYPROBE_SUBS { public TURBIDITY_PROBE TURBIDITYPROBE { get; set; } }
public class SYSTEM_MAN 
{
    public string status { get; set; }
    public SYSTEM SYSTEM { get; set; }
}
public class FILES_REPLY
{
    public string status { get; set; }
    public FILES FILES { get; set; } = new();
}
public class ACID_RESPONSE
{
    public string status { get; set; }
    public ACID ACID { get; set; }
}
#pragma warning restore format