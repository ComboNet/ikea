using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TestWpfReadExpFile1.View;
using System.Dynamic;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace TestWpfReadExpFile1.Model;

#pragma warning disable format
public class Bioreactor : BaseViewModel
{
    private string version;
    public string Version
    {
        get { return version; }
        set
        {
            version = value;
            OnPropertyChanged(nameof(Version));
        }
    }

   
    private SYSTEM system;
    public SYSTEM SYSTEM
    {
        get { return system; }
        set
        {
            system = value;
            OnPropertyChanged(nameof(SYSTEM));
        }
    }

    private EXPERIMENT experiment;
    public EXPERIMENT EXPERIMENT
    {
        get { return experiment; }
        set
        {
            experiment = value;
            OnPropertyChanged(nameof(EXPERIMENT));
        }
    }

    private DO _do;
    public DO DO
    {
        get { return _do; }
        set
        {
            _do = value;
            OnPropertyChanged(nameof(DO));
        }
    }

    private PH ph = new();
    public PH PH
    {
        get { return ph; }
        set
        {
            ph = value;
            OnPropertyChanged(nameof(PH));
        }
    }

    private TEMP temp = new();
    public TEMP TEMP
    {
        get { return temp; }
        set
        {
            temp = value;
            OnPropertyChanged(nameof(TEMP));
        }
    }

    private CHAOTIC chaotic = new();
    public CHAOTIC CHAOTIC
    {
        get { return chaotic; }
        set
        {
            chaotic = value;
            OnPropertyChanged(nameof(CHAOTIC));
        }
    }

    private FEDBATCH fedbatch = new();
    public FEDBATCH FEDBATCH
    {
        get { return fedbatch; }
        set
        {
            fedbatch = value;
            OnPropertyChanged(nameof(FEDBATCH));
        }
    }

    private SETVALUEAUTOMATION setvalueautomation = new SETVALUEAUTOMATION();
    public SETVALUEAUTOMATION SETVALUEAUTOMATION
    {
        get { return setvalueautomation; }
        set
        {
            setvalueautomation = value;
            OnPropertyChanged(nameof(SETVALUEAUTOMATION));
        }
    }

    private STIRRER stirrer = new();
    public STIRRER STIRRER
    {
        get { return stirrer; }
        set
        {
            stirrer = value;
            OnPropertyChanged(nameof(STIRRER));
        }
    }

    private EXHAUST exhaust = new();
    public EXHAUST EXHAUST
    {
        get { return exhaust; }
        set
        {
            exhaust = value;
            OnPropertyChanged(nameof(EXHAUST));
        }
    }

    private FOAM_LIMIT foamlimit = new();
    public FOAM_LIMIT FOAMLIMIT
    {
        get { return foamlimit; }
        set
        {
            foamlimit = value;
            OnPropertyChanged(nameof(FOAMLIMIT));
        }
    }

    private LEVEL_LIMIT levellmit = new();
    public LEVEL_LIMIT LEVELLIMIT
    {
        get { return levellmit; }
        set
        {
            levellmit = value;
            OnPropertyChanged(nameof(LEVELLIMIT));
        }
    }

    /*[Category("Information")]
    [Description("This property uses for ACID")]
    [ExpandableObject]*/
    private ACID acid = new();
    public ACID ACID
    {
        get { return acid; }
        set
        {
            acid = value;
            OnPropertyChanged(nameof(ACID));
        }
    }

    /*[Category("Information")]
    [Description("This property uses for BASE")]
    [ExpandableObject]*/
    private BASE _base = new();
    public BASE BASE
    {
        get { return _base; }
        set
        {
            _base = value;
            OnPropertyChanged(nameof(BASE));
        }
    }

    private AFOAM afoam = new();
    public AFOAM AFOAM
    {
        get { return afoam; }
        set
        {
            afoam = value;
            OnPropertyChanged(nameof(AFOAM));
        }
    }

    private LEVEL level = new();
    public LEVEL LEVEL
    {
        get { return level; }
        set
        {
            level = value;
            OnPropertyChanged(nameof(LEVEL));
        }
    }

    private SUBA suba;
    public SUBA SUBA
    {
        get { return suba; }
        set
        {
            suba = value;
            OnPropertyChanged(nameof(SUBA));
        }
    }

    private AIR air;
    public AIR AIR
    {
        get { return air; }
        set
        {
            air = value;
            OnPropertyChanged(nameof(AIR));
        }
    }

    private O2 o2;
    public O2 O2
    {
        get { return o2; }
        set
        {
            o2 = value;
            OnPropertyChanged(nameof(O2));
        }
    }

    private N2 n2;
    public N2 N2
    {
        get { return n2; }
        set
        {
            n2 = value;
            OnPropertyChanged(nameof(N2));
        }
    }

    private CO2 co2;
    public CO2 CO2
    {
        get { return co2; }
        set
        {
            co2 = value;
            OnPropertyChanged(nameof(CO2));
        }
    }

    private LIGHT light = new();
    public LIGHT LIGHT
    {
        get { return light; }
        set
        {
            light = value;
            OnPropertyChanged(nameof(LIGHT));
        }
    }

    private GAS_MIXER gasmixer = new();
    public GAS_MIXER GASMIXER
    {
        get { return gasmixer; }
        set
        {
            gasmixer = value;
            OnPropertyChanged(nameof(GASMIXER));
        }
    }

    private CO2_PROBE co2probe = new();
    public CO2_PROBE CO2_PROBE
    {
        get { return co2probe; }
        set
        {
            co2probe = value;
            OnPropertyChanged(nameof(CO2_PROBE));
        }
    }

    private EC_PROBE ecprobe = new();
    public EC_PROBE ECPROBE
    {
        get { return ecprobe; }
        set
        {
            ecprobe = value;
            OnPropertyChanged(nameof(ECPROBE));
        }
    }

    private TURBIDITY_PROBE turbidityproble = new();
    public TURBIDITY_PROBE TURBIDITYPROBE
    {
        get { return turbidityproble; }
        set
        {
            turbidityproble = value;
            OnPropertyChanged(nameof(TURBIDITYPROBE));
        }
    }

    private PH_CAL phcal = new();
    public PH_CAL PHCAL
    {
        get { return phcal; }
        set
        {
            phcal = value;
            OnPropertyChanged(nameof(PHCAL));
        }
    }

    private DO_CAL docal = new();
    public DO_CAL DOCAL
    {
        get { return docal; }
        set
        {
            docal = value;
            OnPropertyChanged(nameof(DOCAL));
        }
    }

    private TEMP_CAL tempcal = new();
    public TEMP_CAL TEMPCAL
    {
        get { return tempcal; }
        set
        {
            tempcal = value;
            OnPropertyChanged(nameof(TEMPCAL));
        }
    }

    private ACID_CAL acidcal = new();
    public ACID_CAL ACIDCAL
    {
        get { return acidcal; }
        set
        {
            acidcal = value;
            OnPropertyChanged(nameof(ACIDCAL));
        }
    }

    private BASE_CAL basecal = new();
    public BASE_CAL BASECAL
    {
        get { return basecal; }
        set
        {
            basecal = value;
            OnPropertyChanged(nameof(BASECAL));
        }
    }

    private AFOAM_CAL afoamcal = new();
    public AFOAM_CAL AFOAMCAL
    {
        get { return afoamcal; }
        set
        {
            afoamcal = value;
            OnPropertyChanged(nameof(AFOAMCAL));
        }
    }

    private LEVEL_CAL levelcal = new();
    public LEVEL_CAL LEVELCAL
    {
        get { return levelcal; }
        set
        {
            levelcal = value;
            OnPropertyChanged(nameof(LEVELCAL));
        }
    }

    private SUBA_CAL subacal = new();
    public SUBA_CAL SUBACAL
    {
        get { return subacal; }
        set
        {
            subacal = value;
            OnPropertyChanged(nameof(SUBACAL));
        }
    }

    private TRENDBUFFER trendbuffer;
    public TRENDBUFFER TRENDBUFFER
    {
        get { return trendbuffer; }
        set
        {
            trendbuffer = value;
            OnPropertyChanged(nameof(TRENDBUFFER));
        }
    }

    private BLUEVARY bluevary = new();
    public BLUEVARY BLUEVARY
    {
        get { return bluevary; }
        set
        {
            bluevary = value;
            OnPropertyChanged(nameof(BLUEVARY));
        }
    }

    private CGQBIOR cgqbior = new();
    public CGQBIOR CGQBIOR
    {
        get { return cgqbior; }
        set
        {
            cgqbior = value;
            OnPropertyChanged(nameof(CGQBIOR));
        }
    }

    private WEIGHT weight = new();
    public WEIGHT WEIGHT
    {
        get { return weight; }
        set
        {
            weight = value;
            OnPropertyChanged(nameof(WEIGHT));
        }
    }

    private APPSYNC appsync = new();
    public APPSYNC APPSYNC
    {
        get { return appsync; }
        set
        {
            appsync = value;
            OnPropertyChanged(nameof(APPSYNC));
        }
    }

    private ACTUALPROFILE actualprofile;
    public ACTUALPROFILE ACTUAL_PROFILE
    {
        get { return actualprofile; }
        set
        {
            actualprofile = value;
            OnPropertyChanged(nameof(ACTUAL_PROFILE));
        }
    }

    private ALARMPROFILE alarmprofile;
    public ALARMPROFILE ALARM_PROFILE
    {
        get { return alarmprofile; }
        set
        {
            alarmprofile = value;
            OnPropertyChanged(nameof(ALARM_PROFILE));
        }
    }

    private USERLOGIN userlogin = new();
    public USERLOGIN USERLOGIN
    {
        get { return userlogin; }
        set
        {
            userlogin = value;
            OnPropertyChanged(nameof(USERLOGIN));
        }
    }

    private USERLOGOUT userlogout;
    public USERLOGOUT USERLOGOUT
    {
        get { return userlogout; }
        set
        {
            userlogout = value;
            OnPropertyChanged(nameof(USERLOGOUT));
        }
    }

    private DATALOGGER datalogger;
    public DATALOGGER DATALOGGER
    {
        get { return datalogger; }
        set
        {
            datalogger = value;
            OnPropertyChanged(nameof(DATALOGGER));
        }
    }

    private FILES files;
    public FILES FILES
    {
        get { return files; }
        set
        {
            files = value;
            OnPropertyChanged(nameof(FILES));
        }
    }

    private SAMPLEDATA sampledata = new();
    public SAMPLEDATA SAMPLEDATA
    {
        get { return sampledata; }
        set
        {
            sampledata = value;
            OnPropertyChanged(nameof(SAMPLEDATA));
        }
    }

    private RECIPE recipe;
    public RECIPE RECIPE
    {
        get { return recipe; }
        set
        {
            recipe = value;
            OnPropertyChanged(nameof(RECIPE));
        }
    }

    private SUBS subs;
    public SUBS SUBS
    {
        get { return subs; }
        set
        {
            subs = value;
            OnPropertyChanged(nameof(SUBS));
        }
    }

    private EXCELL _excell = new();
    public EXCELL EXCELL
    {
        get { return _excell; }
        set
        {
            _excell = value;
            OnPropertyChanged(nameof(EXCELL));
        }
    }

    private REDOX _redox = new();
    public REDOX REDOX
    {
        get { return _redox; }
        set
        {
            _redox = value;
            OnPropertyChanged(nameof(REDOX));
        }
    }
}

[ExpandableObject]
public class ACID : BaseViewModel
{
    private int _sts;
    public int sts
    {
        get { return _sts; }
        set
        {
            _sts = value;
            OnPropertyChanged(nameof(sts));
        }
    }

    private int _dir;
    public int dir
    {
        get { return _dir; }
        set
        {
            _dir = value;
            OnPropertyChanged(nameof(dir));
        }
    }

    private int _act_dev;
    public int act_dev
    {
        get { return _act_dev; }
        set
        {
            _act_dev = value;
            OnPropertyChanged(nameof(act_dev));
        }
    }

    private int _vol_sp;
    public int vol_sp
    {
        get { return _vol_sp; }
        set
        {
            _vol_sp = value;
            OnPropertyChanged(nameof(vol_sp));
        }
    }

    private int _vol_pv;
    public int vol_pv
    {
        get { return _vol_pv; }
        set
        {
            _vol_pv = value;
            OnPropertyChanged(nameof(vol_pv));
        }
    }

    private int _flow_sp;
    public int flow_sp
    {
        get { return _flow_sp; }
        set
        {
            _flow_sp = value;
            OnPropertyChanged(nameof(flow_sp));
        }
    }

    private int _flow_pv;
    public int flow_pv
    {
        get { return _flow_pv; }
        set
        {
            _flow_pv = value;
            OnPropertyChanged(nameof(flow_pv));
        }
    }

    private int _alm_enb;
    public int alm_enb
    {
        get { return _alm_enb; }
        set
        {
            _alm_enb = value;
            OnPropertyChanged(nameof(alm_enb));
        }
    }

    private int _alm_sts;
    public int alm_sts
    {
        get { return _alm_sts; }
        set
        {
            _alm_sts = value;
            OnPropertyChanged(nameof(alm_sts));
        }
    }

    private int _alm_llm;
    public int alm_llm
    {
        get { return _alm_llm; }
        set
        {
            _alm_llm = value;
            OnPropertyChanged(nameof(alm_llm));
        }
    }

    private int _alm_ulm;
    public int alm_ulm
    {
        get { return _alm_ulm; }
        set
        {
            _alm_ulm = value;
            OnPropertyChanged(nameof(alm_ulm));
        }
    }

    private ObservableCollection<object> _sp_ramp;
    public ObservableCollection<object> sp_ramp
    {
        get { return _sp_ramp; }
        set
        {
            _sp_ramp = value;
            OnPropertyChanged(nameof(sp_ramp));
        }
    }

    private int _ramp_idx;
    public int ramp_idx
    {
        get { return _ramp_idx; }
        set
        {
            _ramp_idx = value;
            OnPropertyChanged(nameof(ramp_idx));
        }
    }

    private int _ramp_start;
    public int ramp_start
    {
        get { return _ramp_start; }
        set
        {
            _ramp_start = value;
            OnPropertyChanged(nameof(ramp_start));
        }
    }

    private int _ramp_mode;
    public int ramp_mode
    {
        get { return _ramp_mode; }
        set
        {
            _ramp_mode = value;
            OnPropertyChanged(nameof(ramp_mode));
        }
    }

    private int _rem_pv;
    public int rem_pv
    {
        get { return _rem_pv; }
        set
        {
            _rem_pv = value;
            OnPropertyChanged(nameof(rem_pv));
        }
    }
}

public class ACID_CAL : BaseViewModel
{
    private int _sts;
    public int sts
    {
        get { return _sts; }
        set
        {
            _sts = value;
            OnPropertyChanged(nameof(sts));
        }
    }

    private int _time_stamp;
    public int time_stamp
    {
        get { return _time_stamp; }
        set
        {
            _time_stamp = value;
            OnPropertyChanged(nameof(time_stamp));
        }
    }

    private int _slope;
    public int slope
    {
        get { return _slope; }
        set
        {
            _slope = value;
            OnPropertyChanged(nameof(slope));
        }
    }

    private int _tubing;
    public int tubing
    {
        get { return _tubing; }
        set
        {
            _tubing = value;
            OnPropertyChanged(nameof(tubing));
        }
    }

    private int _vol_sp;
    public int vol_sp
    {
        get { return _vol_sp; }
        set
        {
            _vol_sp = value;
            OnPropertyChanged(nameof(vol_sp));
        }
    }

    private int _measure;
    public int measure
    {
        get { return _measure; }
        set
        {
            _measure = value;
            OnPropertyChanged(nameof(measure));
        }
    }

    private int _tube_len;
    public int tube_len
    {
        get { return _tube_len; }
        set
        {
            _tube_len = value;
            OnPropertyChanged(nameof(tube_len));
        }
    }
}

public class ACTUALPROFILE : BaseViewModel
{
    private SYSTEM _system;
    public SYSTEM SYSTEM
    {
        get { return _system; }
        set
        {
            _system = value;
            OnPropertyChanged(nameof(SYSTEM));
        }
    }

    private EXPERIMENT _experiment;
    public EXPERIMENT EXPERIMENT
    {
        get { return _experiment; }
        set
        {
            _experiment = value;
            OnPropertyChanged(nameof(EXPERIMENT));
        }
    }

    private DO _do;
    public DO DO
    {
        get { return _do; }
        set
        {
            _do = value;
            OnPropertyChanged(nameof(DO));
        }
    }

    private PH _ph;
    public PH PH
    {
        get { return _ph; }
        set
        {
            _ph = value;
            OnPropertyChanged(nameof(PH));
        }
    }

    private TEMP _temp;
    public TEMP TEMP
    {
        get { return _temp; }
        set
        {
            _temp = value;
            OnPropertyChanged(nameof(TEMP));
        }
    }

    private FOAM_LIMIT _foamlimit;
    public FOAM_LIMIT FOAMLIMIT
    {
        get { return _foamlimit; }
        set
        {
            _foamlimit = value;
            OnPropertyChanged(nameof(FOAMLIMIT));
        }
    }

    private LEVEL_LIMIT _levellimit;
    public LEVEL_LIMIT LEVELLIMIT
    {
        get { return _levellimit; }
        set
        {
            _levellimit = value;
            OnPropertyChanged(nameof(LEVELLIMIT));
        }
    }

    private STIRRER _stirrer;
    public STIRRER STIRRER
    {
        get { return _stirrer; }
        set
        {
            _stirrer = value;
            OnPropertyChanged(nameof(STIRRER));
        }
    }

    private EXHAUST _exhaust;
    public EXHAUST EXHAUST
    {
        get { return _exhaust; }
        set
        {
            _exhaust = value;
            OnPropertyChanged(nameof(EXHAUST));
        }
    }

    private ACID _acid;
    public ACID ACID
    {
        get { return _acid; }
        set
        {
            _acid = value;
            OnPropertyChanged(nameof(ACID));
        }
    }

    private BASE _base;
    public BASE BASE
    {
        get { return _base; }
        set
        {
            _base = value;
            OnPropertyChanged(nameof(BASE));
        }
    }

    private AFOAM _afoam;
    public AFOAM AFOAM
    {
        get { return _afoam; }
        set
        {
            _afoam = value;
            OnPropertyChanged(nameof(AFOAM));
        }
    }

    private LEVEL _level;
    public LEVEL LEVEL
    {
        get { return _level; }
        set
        {
            _level = value;
            OnPropertyChanged(nameof(LEVEL));
        }
    }

    private SUBA _suba;
    public SUBA SUBA
    {
        get { return _suba; }
        set
        {
            _suba = value;
            OnPropertyChanged(nameof(SUBA));
        }
    }

    private AIR _air;
    public AIR AIR
    {
        get { return _air; }
        set
        {
            _air = value;
            OnPropertyChanged(nameof(AIR));
        }
    }

    private O2 _o2;
    public O2 O2
    {
        get { return _o2; }
        set
        {
            _o2 = value;
            OnPropertyChanged(nameof(O2));
        }
    }

    private N2 _n2;
    public N2 N2
    {
        get { return _n2; }
        set
        {
            _n2 = value;
            OnPropertyChanged(nameof(N2));
        }
    }

    private CO2 _co2;
    public CO2 CO2
    {
        get { return _co2; }
        set
        {
            _co2 = value;
            OnPropertyChanged(nameof(CO2));
        }
    }

    private LIGHT _light;
    public LIGHT LIGHT
    {
        get { return _light; }
        set
        {
            _light = value;
            OnPropertyChanged(nameof(LIGHT));
        }
    }

    private CO2_PROBE _co2proble;
    public CO2_PROBE CO2PROBE
    {
        get { return _co2proble; }
        set
        {
            _co2proble = value;
            OnPropertyChanged(nameof(CO2PROBE));
        }
    }

    private EC_PROBE _ecprobe;
    public EC_PROBE ECPROBE
    {
        get { return _ecprobe; }
        set
        {
            _ecprobe = value;
            OnPropertyChanged(nameof(ECPROBE));
        }
    }

    private TURBIDITY_PROBE _turbidityprobe;
    public TURBIDITY_PROBE TURBIDITYPROBE
    {
        get { return _turbidityprobe; }
        set
        {
            _turbidityprobe = value;
            OnPropertyChanged(nameof(TURBIDITYPROBE));
        }
    }

    private BLUEVARY _bluevary;
    public BLUEVARY BLUEVARY
    {
        get { return _bluevary; }
        set
        {
            _bluevary = value;
            OnPropertyChanged(nameof(BLUEVARY));
        }
    }

    private CGQBIOR _cgqbior;
    public CGQBIOR CGQBIOR
    {
        get { return _cgqbior; }
        set
        {
            _cgqbior = value;
            OnPropertyChanged(nameof(CGQBIOR));
        }
    }
}

public class AFOAM : BaseViewModel
{
    private int _sts;
    public int sts
    {
        get { return _sts; }
        set
        {
            _sts = value;
            OnPropertyChanged(nameof(sts));
        }
    }

    private int _dir;
    public int dir
    {
        get { return _dir; }
        set
        {
            _dir = value;
            OnPropertyChanged(nameof(dir));
        }
    }

    private int _act_dev;
    public int act_dev
    {
        get { return _act_dev; }
        set
        {
            _act_dev = value;
            OnPropertyChanged(nameof(act_dev));
        }
    }

    private int _vol_sp;
    public int vol_sp
    {
        get { return _vol_sp; }
        set
        {
            _vol_sp = value;
            OnPropertyChanged(nameof(vol_sp));
        }
    }

    private int _vol_pv;
    public int vol_pv
    {
        get { return _vol_pv; }
        set
        {
            _vol_pv = value;
            OnPropertyChanged(nameof(vol_pv));
        }
    }

    private int _flow_sp;
    public int flow_sp
    {
        get { return _flow_sp; }
        set
        {
            _flow_sp = value;
            OnPropertyChanged(nameof(flow_sp));
        }
    }

    private int _flow_pv;
    public int flow_pv
    {
        get { return _flow_pv; }
        set
        {
            _flow_pv = value;
            OnPropertyChanged(nameof(flow_pv));
        }
    }

    private int _alm_enb;
    public int alm_enb
    {
        get { return _alm_enb; }
        set
        {
            _alm_enb = value;
            OnPropertyChanged(nameof(alm_enb));
        }
    }

    private int _alm_sts;
    public int alm_sts
    {
        get { return _alm_sts; }
        set
        {
            _alm_sts = value;
            OnPropertyChanged(nameof(alm_sts));
        }
    }

    private int _alm_llm;
    public int alm_llm
    {
        get { return _alm_llm; }
        set
        {
            _alm_llm = value;
            OnPropertyChanged(nameof(alm_llm));
        }
    }

    private int _alm_ulm;
    public int alm_ulm
    {
        get { return _alm_ulm; }
        set
        {
            _alm_ulm = value;
            OnPropertyChanged(nameof(alm_ulm));
        }
    }

    private ObservableCollection<object> _sp_ramp;
    public ObservableCollection<object> sp_ramp
    {
        get { return _sp_ramp; }
        set
        {
            _sp_ramp = value;
            OnPropertyChanged(nameof(sp_ramp));
        }
    }

    private int _ramp_idx;
    public int ramp_idx
    {
        get { return _ramp_idx; }
        set
        {
            _ramp_idx = value;
            OnPropertyChanged(nameof(ramp_idx));
        }
    }

    private int _ramp_start;
    public int ramp_start
    {
        get { return _ramp_start; }
        set
        {
            _ramp_start = value;
            OnPropertyChanged(nameof(ramp_start));
        }
    }

    private int _ramp_mode;
    public int ramp_mode
    {
        get { return _ramp_mode; }
        set
        {
            _ramp_mode = value;
            OnPropertyChanged(nameof(ramp_mode));
        }
    }

    private int _rem_pv;
    public int rem_pv
    {
        get { return _rem_pv; }
        set
        {
            _rem_pv = value;
            OnPropertyChanged(nameof(rem_pv));
        }
    }
}

public class AFOAM_CAL : BaseViewModel
{
    private int _sts;
    public int sts
    {
        get { return _sts; }
        set
        {
            _sts = value;
            OnPropertyChanged(nameof(sts));
        }
    }

    private int _time_stamp;
    public int time_stamp
    {
        get { return _time_stamp; }
        set
        {
            _time_stamp = value;
            OnPropertyChanged(nameof(time_stamp));
        }
    }

    private int _slope;
    public int slope
    {
        get { return _slope; }
        set
        {
            _slope = value;
            OnPropertyChanged(nameof(slope));
        }
    }

    private int _tubing;
    public int tubing
    {
        get { return _tubing; }
        set
        {
            _tubing = value;
            OnPropertyChanged(nameof(tubing));
        }
    }

    private int _vol_sp;
    public int vol_sp
    {
        get { return _vol_sp; }
        set
        {
            _vol_sp = value;
            OnPropertyChanged(nameof(vol_sp));
        }
    }

    private int _measure;
    public int measure
    {
        get { return _measure; }
        set
        {
            _measure = value;
            OnPropertyChanged(nameof(measure));
        }
    }

    private int _tube_len;
    public int tube_len
    {
        get { return _tube_len; }
        set
        {
            _tube_len = value;
            OnPropertyChanged(nameof(tube_len));
        }
    }
}

public class AIR : BaseViewModel
{
    private int _sts;
    public int sts
    {
        get { return _sts; }
        set
        {
            _sts = value;
            OnPropertyChanged(nameof(sts));
        }
    }

    private int _flow_sp;
    public int flow_sp
    {
        get { return _flow_sp; }
        set
        {
            _flow_sp = value;
            OnPropertyChanged(nameof(flow_sp));
        }
    }

    private int _flow_pv;
    public int flow_pv
    {
        get { return _flow_pv; }
        set
        {
            _flow_pv = value;
            OnPropertyChanged(nameof(flow_pv));
        }
    }

    private int _vol_pv;
    public int vol_pv
    {
        get { return _vol_pv; }
        set
        {
            _vol_pv = value;
            OnPropertyChanged(nameof(vol_pv));
        }
    }

    private int _alm_enb;
    public int alm_enb
    {
        get { return _alm_enb; }
        set
        {
            _alm_enb = value;
            OnPropertyChanged(nameof(alm_enb));
        }
    }

    private int _alm_sts;
    public int alm_sts
    {
        get { return _alm_sts; }
        set
        {
            _alm_sts = value;
            OnPropertyChanged(nameof(alm_sts));
        }
    }

    private int _alm_llm;
    public int alm_llm
    {
        get { return _alm_llm; }
        set
        {
            _alm_llm = value;
            OnPropertyChanged(nameof(alm_llm));
        }
    }

    private int _alm_ulm;
    public int alm_ulm
    {
        get { return _alm_ulm; }
        set
        {
            _alm_ulm = value;
            OnPropertyChanged(nameof(alm_ulm));
        }
    }

    private ObservableCollection<object> _sp_ramp;
    public ObservableCollection<object> sp_ramp
    {
        get { return _sp_ramp; }
        set
        {
            _sp_ramp = value;
            OnPropertyChanged(nameof(sp_ramp));
        }
    }

    private int _ramp_idx;
    public int ramp_idx
    {
        get { return _ramp_idx; }
        set
        {
            _ramp_idx = value;
            OnPropertyChanged(nameof(ramp_idx));
        }
    }

    private int _ramp_start;
    public int ramp_start
    {
        get { return _ramp_start; }
        set
        {
            _ramp_start = value;
            OnPropertyChanged(nameof(ramp_start));
        }
    }

    private int _ramp_mode;
    public int ramp_mode
    {
        get { return _ramp_mode; }
        set
        {
            _ramp_mode = value;
            OnPropertyChanged(nameof(ramp_mode));
        }
    }

    private int _rem_pv;
    public int rem_pv
    {
        get { return _rem_pv; }
        set
        {
            _rem_pv = value;
            OnPropertyChanged(nameof(rem_pv));
        }
    }
}

public class ALARMPROFILE : BaseViewModel
{
    private DO _do;
    public DO DO
    {
        get { return _do; }
        set
        {
            _do = value;
            OnPropertyChanged(nameof(DO));
        }
    }

    private PH _ph;
    public PH PH
    {
        get { return _ph; }
        set
        {
            _ph = value;
            OnPropertyChanged(nameof(PH));
        }
    }

    private TEMP _temp;
    public TEMP TEMP
    {
        get { return _temp; }
        set
        {
            _temp = value;
            OnPropertyChanged(nameof(TEMP));
        }
    }

    private STIRRER _stirrer;
    public STIRRER STIRRER
    {
        get { return _stirrer; }
        set
        {
            _stirrer = value;
            OnPropertyChanged(nameof(STIRRER));
        }
    }

    private EXHAUST _exhaust;
    public EXHAUST EXHAUST
    {
        get { return _exhaust; }
        set
        {
            _exhaust = value;
            OnPropertyChanged(nameof(EXHAUST));
        }
    }
}

public class AlmSts { }

public class APPSYNC : BaseViewModel
{
    private SYSTEM _system = new();
    public SYSTEM SYSTEM
    {
        get { return _system; }
        set
        {
            _system = value;
            OnPropertyChanged(nameof(SYSTEM));
        }
    }

    private EXPERIMENT _experiment = new();
    public EXPERIMENT EXPERIMENT
    {
        get { return _experiment; }
        set
        {
            _experiment = value;
            OnPropertyChanged(nameof(EXPERIMENT));
        }
    }

    private STIRRER _stirrer;
    public STIRRER STIRRER
    {
        get { return _stirrer; }
        set
        {
            _stirrer = value;
            OnPropertyChanged(nameof(STIRRER));
        }
    }

    private EXHAUST _exhaust;
    public EXHAUST EXHAUST
    {
        get { return _exhaust; }
        set
        {
            _exhaust = value;
            OnPropertyChanged(nameof(EXHAUST));
        }
    }

    private DO _do;
    public DO DO
    {
        get { return _do; }
        set
        {
            _do = value;
            OnPropertyChanged(nameof(DO));
        }
    }

    private PH _ph;
    public PH PH
    {
        get { return _ph; }
        set
        {
            _ph = value;
            OnPropertyChanged(nameof(PH));
        }
    }

    private TEMP _temp = new();
    public TEMP TEMP
    {
        get { return _temp; }
        set
        {
            _temp = value;
            OnPropertyChanged(nameof(TEMP));
        }
    }

    private FOAM_LIMIT _foamlimit;
    public FOAM_LIMIT FOAMLIMIT
    {
        get { return _foamlimit; }
        set
        {
            _foamlimit = value;
            OnPropertyChanged(nameof(FOAMLIMIT));
        }
    }

    private LEVEL_LIMIT _levellimit;
    public LEVEL_LIMIT LEVELLIMIT
    {
        get { return _levellimit; }
        set
        {
            _levellimit = value;
            OnPropertyChanged(nameof(LEVELLIMIT));
        }
    }

    private ACID _acid;
    public ACID ACID
    {
        get { return _acid; }
        set
        {
            _acid = value;
            OnPropertyChanged(nameof(ACID));
        }
    }

    private BASE _base;
    public BASE BASE
    {
        get { return _base; }
        set
        {
            _base = value;
            OnPropertyChanged(nameof(BASE));
        }
    }

    private AFOAM _afoam;
    public AFOAM AFOAM
    {
        get { return _afoam; }
        set
        {
            _afoam = value;
            OnPropertyChanged(nameof(AFOAM));
        }
    }

    private LEVEL _level;
    public LEVEL LEVEL
    {
        get { return _level; }
        set
        {
            _level = value;
            OnPropertyChanged(nameof(LEVEL));
        }
    }

    private SUBA _suba;
    public SUBA SUBA
    {
        get { return _suba; }
        set
        {
            _suba = value;
            OnPropertyChanged(nameof(SUBA));
        }
    }

    private AIR _air;
    public AIR AIR
    {
        get { return _air; }
        set
        {
            _air = value;
            OnPropertyChanged(nameof(AIR));
        }
    }

    private O2 _o2;
    public O2 O2
    {
        get { return _o2; }
        set
        {
            _o2 = value;
            OnPropertyChanged(nameof(O2));
        }
    }

    private N2 _n2;
    public N2 N2
    {
        get { return _n2; }
        set
        {
            _n2 = value;
            OnPropertyChanged(nameof(N2));
        }
    }

    private CO2 _co2;
    public CO2 CO2
    {
        get { return _co2; }
        set
        {
            _co2 = value;
            OnPropertyChanged(nameof(CO2));
        }
    }

    private LIGHT _light;
    public LIGHT LIGHT
    {
        get { return _light; }
        set
        {
            _light = value;
            OnPropertyChanged(nameof(LIGHT));
        }
    }

    private GAS_MIXER _gasmixer;
    public GAS_MIXER GASMIXER
    {
        get { return _gasmixer; }
        set
        {
            _gasmixer = value;
            OnPropertyChanged(nameof(GASMIXER));
        }
    }

    private PH_CAL _phcal = new();
    public PH_CAL PHCAL
    {
        get { return _phcal; }
        set
        {
            _phcal = value;
            OnPropertyChanged(nameof(PHCAL));
        }
    }

    private DO_CAL _docal = new();
    public DO_CAL DOCAL
    {
        get { return _docal; }
        set
        {
            _docal = value;
            OnPropertyChanged(nameof(DOCAL));
        }
    }

    private TEMP_CAL _tempcal = new();
    public TEMP_CAL TEMPCAL
    {
        get { return _tempcal; }
        set
        {
            _tempcal = value;
            OnPropertyChanged(nameof(TEMPCAL));
        }
    }

    private ACID_CAL _acidcal = new();
    public ACID_CAL ACIDCAL
    {
        get { return _acidcal; }
        set
        {
            _acidcal = value;
            OnPropertyChanged(nameof(ACIDCAL));
        }
    }

    private BASE_CAL _basecal = new();
    public BASE_CAL BASECAL
    {
        get { return _basecal; }
        set
        {
            _basecal = value;
            OnPropertyChanged(nameof(BASECAL));
        }
    }

    private AFOAM_CAL _afoamcal = new();
    public AFOAM_CAL AFOAMCAL
    {
        get { return _afoamcal; }
        set
        {
            _afoamcal = value;
            OnPropertyChanged(nameof(AFOAMCAL));
        }
    }

    private LEVEL_CAL _levelcal = new();
    public LEVEL_CAL LEVELCAL
    {
        get { return _levelcal; }
        set
        {
            _levelcal = value;
            OnPropertyChanged(nameof(LEVELCAL));
        }
    }

    private SUBA_CAL _subacal = new();
    public SUBA_CAL SUBACAL
    {
        get { return _subacal; }
        set
        {
            _subacal = value;
            OnPropertyChanged(nameof(SUBACAL));
        }
    }

    private CHAOTIC _chaotic = new();
    public CHAOTIC CHAOTIC
    {
        get { return _chaotic; }
        set
        {
            _chaotic = value;
            OnPropertyChanged(nameof(CHAOTIC));
        }
    }

    private CO2_PROBE _co2probe;
    public CO2_PROBE CO2PROBE
    {
        get { return _co2probe; }
        set
        {
            _co2probe = value;
            OnPropertyChanged(nameof(CO2PROBE));
        }
    }

    private EC_PROBE _ecprobe;
    public EC_PROBE ECPROBE
    {
        get { return _ecprobe; }
        set
        {
            _ecprobe = value;
            OnPropertyChanged(nameof(ECPROBE));
        }
    }

    private TURBIDITY_PROBE _turbidityprobe;
    public TURBIDITY_PROBE TURBIDITYPROBE
    {
        get { return _turbidityprobe; }
        set
        {
            _turbidityprobe = value;
            OnPropertyChanged(nameof(TURBIDITYPROBE));
        }
    }

    private WEIGHT _weight;
    public WEIGHT WEIGHT
    {
        get { return _weight; }
        set
        {
            _weight = value;
            OnPropertyChanged(nameof(WEIGHT));
        }
    }

}

public class BASE : BaseViewModel
{
    private int _sts;
    public int sts
    {
        get { return _sts; }
        set
        {
            _sts = value;
            OnPropertyChanged(nameof(sts));
        }
    }

    private int _dir;
    public int dir
    {
        get { return _dir; }
        set
        {
            _dir = value;
            OnPropertyChanged(nameof(dir));
        }
    }

    private int _act_dev;
    public int act_dev
    {
        get { return _act_dev; }
        set
        {
            _act_dev = value;
            OnPropertyChanged(nameof(act_dev));
        }
    }

    private int _vol_sp;
    public int vol_sp
    {
        get { return _vol_sp; }
        set
        {
            _vol_sp = value;
            OnPropertyChanged(nameof(vol_sp));
        }
    }

    private int _vol_pv;
    public int vol_pv
    {
        get { return _vol_pv; }
        set
        {
            _vol_pv = value;
            OnPropertyChanged(nameof(vol_pv));
        }
    }

    private int _flow_sp;
    public int flow_sp
    {
        get { return _flow_sp; }
        set
        {
            _flow_sp = value;
            OnPropertyChanged(nameof(flow_sp));
        }
    }

    private int _flow_pv;
    public int flow_pv
    {
        get { return _flow_pv; }
        set
        {
            _flow_pv = value;
            OnPropertyChanged(nameof(flow_pv));
        }
    }

    private int _alm_enb;
    public int alm_enb
    {
        get { return _alm_enb; }
        set
        {
            _alm_enb = value;
            OnPropertyChanged(nameof(alm_enb));
        }
    }

    private int _alm_sts;
    public int alm_sts
    {
        get { return _alm_sts; }
        set
        {
            _alm_sts = value;
            OnPropertyChanged(nameof(alm_sts));
        }
    }

    private int _alm_llm;
    public int alm_llm
    {
        get { return _alm_llm; }
        set
        {
            _alm_llm = value;
            OnPropertyChanged(nameof(alm_llm));
        }
    }

    private int _alm_ulm;
    public int alm_ulm
    {
        get { return _alm_ulm; }
        set
        {
            _alm_ulm = value;
            OnPropertyChanged(nameof(alm_ulm));
        }
    }

    private ObservableCollection<object> _sp_ramp;
    public ObservableCollection<object> sp_ramp
    {
        get { return _sp_ramp; }
        set
        {
            _sp_ramp = value;
            OnPropertyChanged(nameof(sp_ramp));
        }
    }

    private int _ramp_idx;
    public int ramp_idx
    {
        get { return _ramp_idx; }
        set
        {
            _ramp_idx = value;
            OnPropertyChanged(nameof(ramp_idx));
        }
    }

    private int _ramp_start;
    public int ramp_start
    {
        get { return _ramp_start; }
        set
        {
            _ramp_start = value;
            OnPropertyChanged(nameof(ramp_start));
        }
    }

    private int _ramp_mode;
    public int ramp_mode
    {
        get { return _ramp_mode; }
        set
        {
            _ramp_mode = value;
            OnPropertyChanged(nameof(ramp_mode));
        }
    }

    private int _rem_pv;
    public int rem_pv
    {
        get { return _rem_pv; }
        set
        {
            _rem_pv = value;
            OnPropertyChanged(nameof(rem_pv));
        }
    }
}

public class BASE_CAL : BaseViewModel
{
    private int _sts;
    public int sts
    {
        get { return _sts; }
        set
        {
            _sts = value;
            OnPropertyChanged(nameof(sts));
        }
    }

    private int _time_stamp;
    public int time_stamp
    {
        get { return _time_stamp; }
        set
        {
            _time_stamp = value;
            OnPropertyChanged(nameof(time_stamp));
        }
    }

    private int _slope;
    public int slope
    {
        get { return _slope; }
        set
        {
            _slope = value;
            OnPropertyChanged(nameof(slope));
        }
    }

    private int _tubing;
    public int tubing
    {
        get { return _tubing; }
        set
        {
            _tubing = value;
            OnPropertyChanged(nameof(tubing));
        }
    }

    private int _vol_sp;
    public int vol_sp
    {
        get { return _vol_sp; }
        set
        {
            _vol_sp = value;
            OnPropertyChanged(nameof(vol_sp));
        }
    }

    private int _measure;
    public int measure
    {
        get { return _measure; }
        set
        {
            _measure = value;
            OnPropertyChanged(nameof(measure));
        }
    }

    private int _tube_len;
    public int tube_len
    {
        get { return _tube_len; }
        set
        {
            _tube_len = value;
            OnPropertyChanged(nameof(tube_len));
        }
    }
}

[ObservableObject]
public partial class BLUEVARY
{
    [ObservableProperty] public int _alm_sts;
    [ObservableProperty] public string _m1_name = "O2";
    [ObservableProperty] public int _m1_val = 0;
    [ObservableProperty] public string _m2_name = "CO2";
    [ObservableProperty] public int _m2_val = 0;
    [ObservableProperty] public int _pres = 0;
    [ObservableProperty] public int _humid = 0;
    [ObservableProperty] public double _vol_sp = 0;
    [ObservableProperty] public int _our_pv = 0;
    [ObservableProperty] public int _cer_pv = 0;
    [ObservableProperty] public int _rq_pv = 0;
    [ObservableProperty] public double _rq_sp = 0;
}

public class CasSetting
{
    public int stir { get; set; }
    public int air { get; set; }
    public int o2 { get; set; }
    public int n2 { get; set; }
}

[ObservableObject]
public partial class CGQBIOR
{
    [JsonIgnore]
    [ObservableProperty] public int _alm_sts;
    [ObservableProperty] public int _id = 1;
    [JsonIgnore]
    [ObservableProperty] public int _m1_val;
}

public class WEIGHT : BaseViewModel
{
    [JsonIgnore]
    private int _alm_sts;
    public int alm_sts
    {
        get { return _alm_sts; }
        set
        {
            _alm_sts = value;
            OnPropertyChanged(nameof(alm_sts));
        }
    }
    private int _id;
    public int id
    {
        get { return _id; }
        set
        {
            _id = value;
            OnPropertyChanged(nameof(id));
        }
    }
    [JsonIgnore]
    private int _m1_val;
    public int m1_val
    {
        get { return _m1_val; }
        set
        {
            _m1_val = value;
            OnPropertyChanged(nameof(m1_val));
        }
    }
}

public class Ch1
{
    public int source { get; set; }
    public List<object> buffer { get; set; }
}

public class Ch2
{
    public int source { get; set; }
    public List<object> buffer { get; set; }
}

public class Ch3
{
    public int source { get; set; }
    public List<object> buffer { get; set; }
}

public class Ch4
{
    public int source { get; set; }
    public List<object> buffer { get; set; }
}

public class Ch5
{
    public int source { get; set; }
    public List<object> buffer { get; set; }
}

public class Ch6
{
    public int source { get; set; }
    public List<object> buffer { get; set; }
}

public class CHAOTIC
{
    public int sp_min { get; set; }
    public int sp_max { get; set; }
    public int duration { get; set; }
}

public class CO2 : BaseViewModel
{
    private int _sts;
    public int sts
    {
        get { return _sts; }
        set
        {
            _sts = value;
            OnPropertyChanged(nameof(sts));
        }
    }

    private int _flow_sp;
    public int flow_sp
    {
        get { return _flow_sp; }
        set
        {
            _flow_sp = value;
            OnPropertyChanged(nameof(flow_sp));
        }
    }

    private int _flow_pv;
    public int flow_pv
    {
        get { return _flow_pv; }
        set
        {
            _flow_pv = value;
            OnPropertyChanged(nameof(flow_pv));
        }
    }

    private int _vol_pv;
    public int vol_pv
    {
        get { return _vol_pv; }
        set
        {
            _vol_pv = value;
            OnPropertyChanged(nameof(vol_pv));
        }
    }

    private int _alm_enb;
    public int alm_enb
    {
        get { return _alm_enb; }
        set
        {
            _alm_enb = value;
            OnPropertyChanged(nameof(alm_enb));
        }
    }

    private int _alm_sts;
    public int alm_sts
    {
        get { return _alm_sts; }
        set
        {
            _alm_sts = value;
            OnPropertyChanged(nameof(alm_sts));
        }
    }

    private int _alm_llm;
    public int alm_llm
    {
        get { return _alm_llm; }
        set
        {
            _alm_llm = value;
            OnPropertyChanged(nameof(alm_llm));
        }
    }

    private int _alm_ulm;
    public int alm_ulm
    {
        get { return _alm_ulm; }
        set
        {
            _alm_ulm = value;
            OnPropertyChanged(nameof(alm_ulm));
        }
    }

    private ObservableCollection<object> _sp_ramp;
    public ObservableCollection<object> sp_ramp
    {
        get { return _sp_ramp; }
        set
        {
            _sp_ramp = value;
            OnPropertyChanged(nameof(sp_ramp));
        }
    }

    private int _ramp_idx;
    public int ramp_idx
    {
        get { return _ramp_idx; }
        set
        {
            _ramp_idx = value;
            OnPropertyChanged(nameof(ramp_idx));
        }
    }

    private int _ramp_start;
    public int ramp_start
    {
        get { return _ramp_start; }
        set
        {
            _ramp_start = value;
            OnPropertyChanged(nameof(ramp_start));
        }
    }

    private int _ramp_mode;
    public int ramp_mode
    {
        get { return _ramp_mode; }
        set
        {
            _ramp_mode = value;
            OnPropertyChanged(nameof(ramp_mode));
        }
    }

    private Pv _pv;
    public Pv PV
    {
        get { return _pv; }
        set
        {
            _pv = value;
            OnPropertyChanged(nameof(PV));
        }
    }
}

public class CO2_PROBE : BaseViewModel
{
    private int _sts;
    public int sts
    {
        get { return _sts; }
        set
        {
            _sts = value;
            OnPropertyChanged(nameof(sts));
        }
    }
    private int _pv;
    public int pv
    {
        get { return _pv; }
        set
        {
            _pv = value;
            OnPropertyChanged(nameof(pv));
        }
    }
    private int _alm_enb;
    public int alm_enb
    {
        get { return _alm_enb; }
        set
        {
            _alm_enb = value;
            OnPropertyChanged(nameof(alm_enb));
        }
    }

    private int _alm_sts;
    public int alm_sts
    {
        get { return _alm_sts; }
        set
        {
            _alm_sts = value;
            OnPropertyChanged(nameof(alm_sts));
        }
    }

    private int _alm_llm;
    public int alm_llm
    {
        get { return _alm_llm; }
        set
        {
            _alm_llm = value;
            OnPropertyChanged(nameof(alm_llm));
        }
    }

    private int _alm_ulm;
    public int alm_ulm
    {
        get { return _alm_ulm; }
        set
        {
            _alm_ulm = value;
            OnPropertyChanged(nameof(alm_ulm));
        }
    }

    private int _sp_min;
    public int sp_min
    {
        get { return _sp_min; }
        set
        {
            _sp_min = value;
            OnPropertyChanged(nameof(sp_min));
        }
    }

    private int _sp_max;
    public int sp_max
    {
        get { return _sp_max; }
        set
        {
            _sp_max = value;
            OnPropertyChanged(nameof(sp_max));
        }
    }

    private int _sp_offset;
    public int sp_offset
    {
        get { return _sp_offset; }
        set
        {
            _sp_offset = value;
            OnPropertyChanged(nameof(sp_offset));
        }
    }

    private int _unit;
    public int unit
    {
        get { return _unit; }
        set
        {
            _unit = value;
            OnPropertyChanged(nameof(unit));
        }
    }
}

public class DATALOGGER
{
    public int sts { get; set; }
    public int start_time { get; set; }
    public int saving_location { get; set; }
    public List<File> files { get; set; }
}

public class Dir { }

public class DO : BaseViewModel
{
    private int _sts;
    public int sts
    {
        get { return _sts; }
        set
        {
            _sts = value;
            OnPropertyChanged(nameof(sts));
        }
    }

    private int _sp;
    public int sp
    {
        get { return _sp; }
        set
        {
            _sp = value;
            OnPropertyChanged(nameof(sp));
        }
    }

    private int _sp_offset;
    public int sp_offset
    {
        get { return _sp_offset; }
        set
        {
            _sp_offset = value;
            OnPropertyChanged(nameof(sp_offset));
        }
    }

    private int _pv;
    public int pv
    {
        get { return _pv; }
        set
        {
            _pv = value;
            OnPropertyChanged(nameof(pv));
        }
    }

    private int _alm_enb;
    public int alm_enb
    {
        get { return _alm_enb; }
        set
        {
            _alm_enb = value;
            OnPropertyChanged(nameof(alm_enb));
        }
    }

    private int _alm_sts;
    public int alm_sts
    {
        get { return _alm_sts; }
        set
        {
            _alm_sts = value;
            OnPropertyChanged(nameof(alm_sts));
        }
    }

    private int _alm_llm;
    public int alm_llm
    {
        get { return _alm_llm; }
        set
        {
            _alm_llm = value;
            OnPropertyChanged(nameof(alm_llm));
        }
    }

    private int _alm_ulm;
    public int alm_ulm
    {
        get { return _alm_ulm; }
        set
        {
            _alm_ulm = value;
            OnPropertyChanged(nameof(alm_ulm));
        }
    }

    private ObservableCollection<object> _sp_ramp;
    public ObservableCollection<object> sp_ramp
    {
        get { return _sp_ramp; }
        set
        {
            _sp_ramp = value;
            OnPropertyChanged(nameof(sp_ramp));
        }
    }

    private int _ramp_idx;
    public int ramp_idx
    {
        get { return _ramp_idx; }
        set
        {
            _ramp_idx = value;
            OnPropertyChanged(nameof(ramp_idx));
        }
    }

    private int _ramp_start;
    public int ramp_start
    {
        get { return _ramp_start; }
        set
        {
            _ramp_start = value;
            OnPropertyChanged(nameof(ramp_start));
        }
    }

    private int _ramp_mode;
    public int ramp_mode
    {
        get { return _ramp_mode; }
        set
        {
            _ramp_mode = value;
            OnPropertyChanged(nameof(ramp_mode));
        }
    }

    private ObservableCollection<CasSetting> _cas_settings;
    public ObservableCollection<CasSetting> cas_settings
    {
        get { return _cas_settings; }
        set
        {
            _cas_settings = value;
            OnPropertyChanged(nameof(cas_settings));
        }
    }

    private int _kp;
    public int kp
    {
        get { return _kp; }
        set
        {
            _kp = value;
            OnPropertyChanged(nameof(kp));
        }
    }

    private int _ki;
    public int ki
    {
        get { return _ki; }
        set
        {
            _ki = value;
            OnPropertyChanged(nameof(ki));
        }
    }

    private int _kd;
    public int kd
    {
        get { return _kd; }
        set
        {
            _kd = value;
            OnPropertyChanged(nameof(kd));
        }
    }

    private int _kp_default;
    public int kp_default
    {
        get { return _kp_default; }
        set
        {
            _kp_default = value;
            OnPropertyChanged(nameof(kp_default));
        }
    }

    private int _ki_default;
    public int ki_default
    {
        get { return _ki_default; }
        set
        {
            _ki_default = value;
            OnPropertyChanged(nameof(ki_default));
        }
    }

    private int _kd_default;
    public int kd_default
    {
        get { return _kd_default; }
        set
        {
            _kd_default = value;
            OnPropertyChanged(nameof(kd_default));
        }
    }

    private int _deadband;
    public int deadband
    {
        get { return _deadband; }
        set
        {
            _deadband = value;
            OnPropertyChanged(nameof(deadband));
        }
    }

    private int _use_digital;
    public int use_digital
    {
        get { return _use_digital; }
        set
        {
            _use_digital = value;
            OnPropertyChanged(nameof(use_digital));
        }
    }

    private int _id;
    public int id
    {
        get { return _id; }
        set
        {
            _id = value;
            OnPropertyChanged(nameof(id));
        }
    }
}

public class File
{
    public string name { get; set; }
    public int create_date { get; set; }
    public int size { get; set; }
}

public class DO_CAL : BaseViewModel
{
    private int _sts;
    public int sts
    {
        get { return _sts; }
        set
        {
            _sts = value;
            OnPropertyChanged(nameof(sts));
        }
    }

    private int _time_stamp;
    public int time_stamp
    {
        get { return _time_stamp; }
        set
        {
            _time_stamp = value;
            OnPropertyChanged(nameof(time_stamp));
        }
    }

    private int _zero;
    public int zero
    {
        get { return _zero; }
        set
        {
            _zero = value;
            OnPropertyChanged(nameof(zero));
        }
    }

    private int _slope;
    public int slope
    {
        get { return _slope; }
        set
        {
            _slope = value;
            OnPropertyChanged(nameof(slope));
        }
    }
    private int _measure;
    public int measure
    {
        get { return _measure; }
        set
        {
            _measure = value;
            OnPropertyChanged(nameof(measure));
        }
    }

    private int _temp_ref;
    public int temp_ref
    {
        get { return _temp_ref; }
        set
        {
            _temp_ref = value;
            OnPropertyChanged(nameof(temp_ref));
        }
    }

    private int _press_ref;
    public int press_ref
    {
        get { return _press_ref; }
        set
        {
            _press_ref = value;
            OnPropertyChanged(nameof(press_ref));
        }
    }

    private int _condition;
    public int condition
    {
        get { return _condition; }
        set
        {
            _condition = value;
            OnPropertyChanged(nameof(condition));
        }
    }

    private string _snr;
    public string snr
    {
        get { return _snr; }
        set
        {
            _snr = value;
            OnPropertyChanged(nameof(snr));
        }
    }
}

public class EC_PROBE : BaseViewModel
{
    private int _sts;
    public int sts
    {
        get { return _sts; }
        set
        {
            _sts = value;
            OnPropertyChanged(nameof(sts));
        }
    }
    private int _pv;
    public int pv
    {
        get { return _pv; }
        set
        {
            _pv = value;
            OnPropertyChanged(nameof(pv));
        }
    }
    private int _alm_enb;
    public int alm_enb
    {
        get { return _alm_enb; }
        set
        {
            _alm_enb = value;
            OnPropertyChanged(nameof(alm_enb));
        }
    }

    private int _alm_sts;
    public int alm_sts
    {
        get { return _alm_sts; }
        set
        {
            _alm_sts = value;
            OnPropertyChanged(nameof(alm_sts));
        }
    }

    private int _alm_llm;
    public int alm_llm
    {
        get { return _alm_llm; }
        set
        {
            _alm_llm = value;
            OnPropertyChanged(nameof(alm_llm));
        }
    }

    private int _alm_ulm;
    public int alm_ulm
    {
        get { return _alm_ulm; }
        set
        {
            _alm_ulm = value;
            OnPropertyChanged(nameof(alm_ulm));
        }
    }

    private int _sp_min;
    public int sp_min
    {
        get { return _sp_min; }
        set
        {
            _sp_min = value;
            OnPropertyChanged(nameof(sp_min));
        }
    }

    private int _sp_max;
    public int sp_max
    {
        get { return _sp_max; }
        set
        {
            _sp_max = value;
            OnPropertyChanged(nameof(sp_max));
        }
    }

    private int _sp_offset;
    public int sp_offset
    {
        get { return _sp_offset; }
        set
        {
            _sp_offset = value;
            OnPropertyChanged(nameof(sp_offset));
        }
    }

    private int _unit;
    public int unit
    {
        get { return _unit; }
        set
        {
            _unit = value;
            OnPropertyChanged(nameof(unit));
        }
    }
}

public class EXHAUST : BaseViewModel
{
    private int _sts;
    public int sts
    {
        get { return _sts; }
        set
        {
            _sts = value;
            OnPropertyChanged(nameof(sts));
        }
    }

    private int _sp;
    public int sp
    {
        get { return _sp; }
        set
        {
            _sp = value;
            OnPropertyChanged(nameof(sp));
        }
    }

    private int _pv;
    public int pv
    {
        get { return _pv; }
        set
        {
            _pv = value;
            OnPropertyChanged(nameof(pv));
        }
    }

    private int _alm_enb;
    public int alm_enb
    {
        get { return _alm_enb; }
        set
        {
            _alm_enb = value;
            OnPropertyChanged(nameof(alm_enb));
        }
    }

    private int _alm_sts;
    public int alm_sts
    {
        get { return _alm_sts; }
        set
        {
            _alm_sts = value;
            OnPropertyChanged(nameof(alm_sts));
        }
    }

    private int _alm_lim;
    public int alm_lim
    {
        get { return _alm_lim; }
        set
        {
            _alm_lim = value;
            OnPropertyChanged(nameof(alm_lim));
        }
    }

    private int _alm_ulm;
    public int alm_ulm
    {
        get { return _alm_ulm; }
        set
        {
            _alm_ulm = value;
            OnPropertyChanged(nameof(alm_ulm));
        }
    }

    private int _pel_sts;
    public int pel_sts
    {
        get { return _pel_sts; }
        set
        {
            _pel_sts = value;
            OnPropertyChanged(nameof(pel_sts));
        }
    }

    private int _pel_temp = 10;
    public int pel_temp
    {
        get { return _pel_temp; }
        set
        {
            _pel_temp = value;
            OnPropertyChanged(nameof(pel_temp));
        }
    }

    private ObservableCollection<SpRamp> _sp_ramp;
    public ObservableCollection<SpRamp> sp_ramp
    {
        get { return _sp_ramp; }
        set
        {
            _sp_ramp = value;
            OnPropertyChanged(nameof(sp_ramp));
        }
    }

    private int _ramp_idx;
    public int ramp_idx
    {
        get { return _ramp_idx; }
        set
        {
            _ramp_idx = value;
            OnPropertyChanged(nameof(ramp_idx));
        }
    }

    private int _ramp_start;
    public int ramp_start
    {
        get { return _ramp_start; }
        set
        {
            _ramp_start = value;
            OnPropertyChanged(nameof(ramp_start));
        }
    }

    private int _ramp_mode;
    public int ramp_mode
    {
        get { return _ramp_mode; }
        set
        {
            _ramp_mode = value;
            OnPropertyChanged(nameof(ramp_mode));
        }
    }
}

public class EXPERIMENT : BaseViewModel
{
    private int _id;
    public int id
    {
        get { return _id; }
        set
        {
            _id = value;
            OnPropertyChanged(nameof(id));
        }
    }

    private int _sts;
    public int sts
    {
        get { return _sts; }
        set
        {
            _sts = value;
            OnPropertyChanged(nameof(sts));
        }
    }

    private string _name;
    public string name
    {
        get { return _name; }
        set
        {
            _name = value;
            OnPropertyChanged(nameof(name));
        }
    }

    private string _author;
    public string author
    {
        get { return _author; }
        set
        {
            _author = value;
            OnPropertyChanged(nameof(author));
        }
    }

    private string _recipe;
    public string recipe
    {
        get { return _recipe; }
        set
        {
            _recipe = value;
            OnPropertyChanged(nameof(recipe));
        }
    }

    private int _vessel;
    public int vessel
    {
        get { return _vessel; }
        set
        {
            _vessel = value;
            OnPropertyChanged(nameof(vessel));
        }
    }

    private string _log;
    public string log
    {
        get { return _log; }
        set
        {
            _log = value;
            OnPropertyChanged(nameof(log));
        }
    }

    private int _interval;
    public int interval
    {
        get { return _interval; }
        set
        {
            _interval = value;
            OnPropertyChanged(nameof(interval));
        }
    }

    private int _startdate;
    public int startdate
    {
        get { return _startdate; }
        set
        {
            _startdate = value;
            OnPropertyChanged(nameof(startdate));
        }
    }

    private int _cultivation;
    public int cultivation
    {
        get { return _cultivation; }
        set
        {
            _cultivation = value;
            OnPropertyChanged(nameof(cultivation));
        }
    }

    private int _stopdate;
    public int stopdate
    {
        get { return _stopdate; }
        set
        {
            _stopdate = value;
            OnPropertyChanged(nameof(stopdate));
        }
    }

    private string _note;
    public string note
    {
        get { return _note; }
        set
        {
            _note = value;
            OnPropertyChanged(nameof(note));
        }
    }

    private int _enabled;
    public int enabled
    {
        get { return _enabled; }
        set
        {
            _enabled = value;
            OnPropertyChanged(nameof(enabled));
        }
    }
}

public class FEDBATCH : BaseViewModel
{
    private int _mode;
    public int mode
    {
        get { return _mode; }
        set
        {
            _mode = value;
            OnPropertyChanged(nameof(mode));
        }
    }

    private int _index;
    public int index
    {
        get { return _index; }
        set
        {
            _index = value;
            OnPropertyChanged(nameof(index));
        }
    }

    private int _cond;
    public int cond
    {
        get { return _cond; }
        set
        {
            _cond = value;
            OnPropertyChanged(nameof(cond));
        }
    }

    private int _ramp;
    public int ramp
    {
        get { return _ramp; } set
        {
            _ramp = value;
            OnPropertyChanged(nameof(ramp));
        }
    }

    private ObservableCollection<int> _duration;
    public ObservableCollection<int> duration
    {
        get { return _duration; }
        set
        {
            _duration = value;
            OnPropertyChanged(nameof(duration));
        }
    }


    private ObservableCollection<object> _temp;
    public ObservableCollection<object> temp
    {
        get { return _temp; }
        set
        {
            _temp = value;
            OnPropertyChanged(nameof(temp));
        }
    }

    private ObservableCollection<object> _ph;
    public ObservableCollection<object> ph
    {
        get { return _ph; }
        set
        {
            _ph = value;
            OnPropertyChanged(nameof(ph));
        }
    }

    private ObservableCollection<object> _dos;
    public ObservableCollection<object> dos
    {
        get { return _dos; }
        set
        {
            _dos = value;
            OnPropertyChanged(nameof(dos));
        }
    }

    private ObservableCollection<object> _level;
    public ObservableCollection<object> level
    {
        get { return _level; }
        set
        {
            _level = value;
            OnPropertyChanged(nameof(level));
        }
    }

    private ObservableCollection<object> _suba;
    public ObservableCollection<object> suba
    {
        get { return _suba; }
        set
        {
            _suba = value;
            OnPropertyChanged(nameof(suba));
        }
    }
}

public class FILES
{
    public int type { get; set; }
    public string name { get; set; }
    public int? size { get; set; }
    public int dest { get; set; }
    public int progress { get; set; }
}

public class FlowPv { }

public class FOAM_LIMIT : BaseViewModel
{
    private int _sts;
    public int sts
    {
        get { return _sts; }
        set
        {
            _sts = value;
            OnPropertyChanged(nameof(sts));
        }
    }

    private int _pv;
    public int pv
    {
        get { return _pv; }
        set
        {
            _pv = value;
            OnPropertyChanged(nameof(pv));
        }
    }

    private int _sensitivity;
    public int sensitivity
    {
        get { return _sensitivity; }
        set
        {
            _sensitivity = value;
            OnPropertyChanged(nameof(sensitivity));
        }
    }

    private int _dead_time;
    public int dead_time
    {
        get { return _dead_time; }
        set
        {
            _dead_time = value;
            OnPropertyChanged(nameof(dead_time));
        }
    }

    private int _pulse_time;
    public int pulse_time
    {
        get { return _pulse_time; }
        set
        {
            _pulse_time = value;
            OnPropertyChanged(nameof(pulse_time));
        }
    }

    private int _alm_enb;
    public int alm_enb
    {
        get { return _alm_enb; }
        set
        {
            _alm_enb = value;
            OnPropertyChanged(nameof(alm_enb));
        }
    }

    private int _alm_sts;
    public int alm_sts
    {
        get { return _alm_sts; }
        set
        {
            _alm_sts = value;
            OnPropertyChanged(nameof(alm_sts));
        }
    }

    private int _alm_dly;
    public int alm_dly
    {
        get { return _alm_dly; }
        set
        {
            _alm_dly = value;
            OnPropertyChanged(nameof(alm_dly));
        }
    }
}

public class GAS_MIXER : BaseViewModel
{
    private int _sts;
    public int sts
    {
        get { return _sts; }
        set
        {
            _sts = value;
            OnPropertyChanged(nameof(sts));
        }
    }

    private int _sp;
    public int sp
    {
        get { return _sp; }
        set
        {
            _sp = value;
            OnPropertyChanged(nameof(sp));
        }
    }

    private int _ratio_air;
    public int ratio_air
    {
        get { return _ratio_air; }
        set
        {
            _ratio_air = value;
            OnPropertyChanged(nameof(ratio_air));
        }
    }

    private int _ratio_o2;
    public int ratio_o2
    {
        get { return _ratio_o2; }
        set
        {
            _ratio_o2 = value;
            OnPropertyChanged(nameof(ratio_o2));
        }
    }
}

public class HtrPv { }

public class Humid { }

public class Id { }

public class LEVEL : BaseViewModel
{
    private int _sts;
    public int sts
    {
        get { return _sts; }
        set
        {
            _sts = value;
            OnPropertyChanged(nameof(sts));
        }
    }

    private int _dir;
    public int dir
    {
        get { return _dir; }
        set
        {
            _dir = value;
            OnPropertyChanged(nameof(dir));
        }
    }

    private int _act_dev;
    public int act_dev
    {
        get { return _act_dev; }
        set
        {
            _act_dev = value;
            OnPropertyChanged(nameof(act_dev));
        }
    }

    private int _vol_sp;
    public int vol_sp
    {
        get { return _vol_sp; }
        set
        {
            _vol_sp = value;
            OnPropertyChanged(nameof(vol_sp));
        }
    }

    private int _vol_pv;
    public int vol_pv
    {
        get { return _vol_pv; }
        set
        {
            _vol_pv = value;
            OnPropertyChanged(nameof(vol_pv));
        }
    }

    private int _flow_sp;
    public int flow_sp
    {
        get { return _flow_sp; }
        set
        {
            _flow_sp = value;
            OnPropertyChanged(nameof(flow_sp));
        }
    }

    private int _flow_pv;
    public int flow_pv
    {
        get { return _flow_pv; }
        set
        {
            _flow_pv = value;
            OnPropertyChanged(nameof(flow_pv));
        }
    }

    private int _alm_enb;
    public int alm_enb
    {
        get { return _alm_enb; }
        set
        {
            _alm_enb = value;
            OnPropertyChanged(nameof(alm_enb));
        }
    }

    private int _alm_sts;
    public int alm_sts
    {
        get { return _alm_sts; }
        set
        {
            _alm_sts = value;
            OnPropertyChanged(nameof(alm_sts));
        }
    }

    private int _alm_llm;
    public int alm_llm
    {
        get { return _alm_llm; }
        set
        {
            _alm_llm = value;
            OnPropertyChanged(nameof(alm_llm));
        }
    }

    private int _alm_ulm;
    public int alm_ulm
    {
        get { return _alm_ulm; }
        set
        {
            _alm_ulm = value;
            OnPropertyChanged(nameof(alm_ulm));
        }
    }

    private ObservableCollection<object> _sp_ramp;
    public ObservableCollection<object> sp_ramp
    {
        get { return _sp_ramp; }
        set
        {
            _sp_ramp = value;
            OnPropertyChanged(nameof(sp_ramp));
        }
    }

    private int _ramp_idx;
    public int ramp_idx
    {
        get { return _ramp_idx; }
        set
        {
            _ramp_idx = value;
            OnPropertyChanged(nameof(ramp_idx));
        }
    }

    private int _ramp_start;
    public int ramp_start
    {
        get { return _ramp_start; }
        set
        {
            _ramp_start = value;
            OnPropertyChanged(nameof(ramp_start));
        }
    }

    private int _ramp_mode;
    public int ramp_mode
    {
        get { return _ramp_mode; }
        set
        {
            _ramp_mode = value;
            OnPropertyChanged(nameof(ramp_mode));
        }
    }

    private int _rem_pv;
    public int rem_pv
    {
        get { return _rem_pv; }
        set
        {
            _rem_pv = value;
            OnPropertyChanged(nameof(rem_pv));
        }
    }
}

public class LEVEL_CAL : BaseViewModel
{
    private int _sts;
    public int sts
    {
        get { return _sts; }
        set
        {
            _sts = value;
            OnPropertyChanged(nameof(sts));
        }
    }

    private int _time_stamp;
    public int time_stamp
    {
        get { return _time_stamp; }
        set
        {
            _time_stamp = value;
            OnPropertyChanged(nameof(time_stamp));
        }
    }

    private int _slope;
    public int slope
    {
        get { return _slope; }
        set
        {
            _slope = value;
            OnPropertyChanged(nameof(slope));
        }
    }

    private int _tubing;
    public int tubing
    {
        get { return _tubing; }
        set
        {
            _tubing = value;
            OnPropertyChanged(nameof(tubing));
        }
    }

    private int _vol_sp;
    public int vol_sp
    {
        get { return _vol_sp; }
        set
        {
            _vol_sp = value;
            OnPropertyChanged(nameof(vol_sp));
        }
    }

    private int _measure;
    public int measure
    {
        get { return _measure; }
        set
        {
            _measure = value;
            OnPropertyChanged(nameof(measure));
        }
    }

    private int _tube_len;
    public int tube_len
    {
        get { return _tube_len; }
        set
        {
            _tube_len = value;
            OnPropertyChanged(nameof(tube_len));
        }
    }
}

public class LEVEL_LIMIT : BaseViewModel
{
    private int _sts;
    public int sts
    {
        get { return _sts; }
        set
        {
            _sts = value;
            OnPropertyChanged(nameof(sts));
        }
    }

    private int _pv;
    public int pv
    {
        get { return _pv; }
        set
        {
            _pv = value;
            OnPropertyChanged(nameof(pv));
        }
    }

    private int _sensitivity;
    public int sensitivity
    {
        get { return _sensitivity; }
        set
        {
            _sensitivity = value;
            OnPropertyChanged(nameof(sensitivity));
        }
    }

    private int _dead_time;
    public int dead_time
    {
        get { return _dead_time; }
        set
        {
            _dead_time = value;
            OnPropertyChanged(nameof(dead_time));
        }
    }

    private int _pulse_time;
    public int pulse_time
    {
        get { return _pulse_time; }
        set
        {
            _pulse_time = value;
            OnPropertyChanged(nameof(pulse_time));
        }
    }

    private int _dir;
    public int dir
    {
        get { return _dir; }
        set
        {
            _dir = value;
            OnPropertyChanged(nameof(dir));
        }
    }

    private int _alm_enb;
    public int alm_enb
    {
        get { return _alm_enb; }
        set
        {
            _alm_enb = value;
            OnPropertyChanged(nameof(alm_enb));
        }
    }

    private int _alm_sts;
    public int alm_sts
    {
        get { return _alm_sts; }
        set
        {
            _alm_sts = value;
            OnPropertyChanged(nameof(alm_sts));
        }
    }

    private int _alm_dly;
    public int alm_dly
    {
        get { return _alm_dly; }
        set
        {
            _alm_dly = value;
            OnPropertyChanged(nameof(alm_dly));
        }
    }
}

public class LIGHT : BaseViewModel
{
    private int _sts;
    public int sts
    {
        get { return _sts; }
        set
        {
            _sts = value;
            OnPropertyChanged(nameof(sts));
        }
    }

    private double _sp;
    public double sp
    {
        get { return _sp; }
        set
        {
            _sp = value;
            OnPropertyChanged(nameof(sp));
        }
    }

    private int _alm_enb;
    public int alm_enb
    {
        get { return _alm_enb; }
        set
        {
            _alm_enb = value;
            OnPropertyChanged(nameof(alm_enb));
        }
    }

    private int _alm_sts;
    public int alm_sts
    {
        get { return _alm_sts; }
        set
        {
            _alm_sts = value;
            OnPropertyChanged(nameof(alm_sts));
        }
    }

    private ObservableCollection<object> _sp_ramp;
    public ObservableCollection<object> sp_ramp
    {
        get { return _sp_ramp; }
        set
        {
            _sp_ramp = value;
            OnPropertyChanged(nameof(sp_ramp));
        }
    }

    private int _ramp_idx;
    public int ramp_idx
    {
        get { return _ramp_idx; }
        set
        {
            _ramp_idx = value;
            OnPropertyChanged(nameof(ramp_idx));
        }
    }

    private int _ramp_start;
    public int ramp_start
    {
        get { return _ramp_start; }
        set
        {
            _ramp_start = value;
            OnPropertyChanged(nameof(ramp_start));
        }
    }

    private int _ramp_mode;
    public int ramp_mode
    {
        get { return _ramp_mode; }
        set
        {
            _ramp_mode = value;
            OnPropertyChanged(nameof(ramp_mode));
        }
    }

    private int _act_dev;
    public int act_dev
    {
        get { return _act_dev; }
        set
        {
            _act_dev = value;
            OnPropertyChanged(nameof(act_dev));
        }
    }

    private string _rgb;
    public string rgb
    {
        get { return _rgb; }
        set
        {
            _rgb = value;
            OnPropertyChanged(nameof(rgb));
        }
    }

    private string _t_on;
    public string t_on
    {
        get { return _t_on; }
        set
        {
            _t_on = value;
            OnPropertyChanged(nameof(t_on));
        }
    }

    private string _t_off;
    public string t_off
    {
        get { return _t_off; }
        set
        {
            _t_off = value;
            OnPropertyChanged(nameof(t_off));
        }
    }
}

public class M1Name { }

public class M1Val { }

public class M2Name { }

public class M2Val { }

public class N2 : BaseViewModel
{
    private int _sts;
    public int sts
    {
        get { return _sts; }
        set
        {
            _sts = value;
            OnPropertyChanged(nameof(sts));
        }
    }

    private int _flow_sp;
    public int flow_sp
    {
        get { return _flow_sp; }
        set
        {
            _flow_sp = value;
            OnPropertyChanged(nameof(flow_sp));
        }
    }

    private int _flow_pv;
    public int flow_pv
    {
        get { return _flow_pv; }
        set
        {
            _flow_pv = value;
            OnPropertyChanged(nameof(flow_pv));
        }
    }

    private int _vol_pv;
    public int vol_pv
    {
        get { return _vol_pv; }
        set
        {
            _vol_pv = value;
            OnPropertyChanged(nameof(vol_pv));
        }
    }

    private int _alm_enb;
    public int alm_enb
    {
        get { return _alm_enb; }
        set
        {
            _alm_enb = value;
            OnPropertyChanged(nameof(alm_enb));
        }
    }

    private int _alm_sts;
    public int alm_sts
    {
        get { return _alm_sts; }
        set
        {
            _alm_sts = value;
            OnPropertyChanged(nameof(alm_sts));
        }
    }

    private int _alm_llm;
    public int alm_llm
    {
        get { return _alm_llm; }
        set
        {
            _alm_llm = value;
            OnPropertyChanged(nameof(alm_llm));
        }
    }

    private int _alm_ulm;
    public int alm_ulm
    {
        get { return _alm_ulm; }
        set
        {
            _alm_ulm = value;
            OnPropertyChanged(nameof(alm_ulm));
        }
    }

    private ObservableCollection<object> _sp_ramp;
    public ObservableCollection<object> sp_ramp
    {
        get { return _sp_ramp; }
        set
        {
            _sp_ramp = value;
            OnPropertyChanged(nameof(sp_ramp));
        }
    }

    private int _ramp_idx;
    public int ramp_idx
    {
        get { return _ramp_idx; }
        set
        {
            _ramp_idx = value;
            OnPropertyChanged(nameof(ramp_idx));
        }
    }

    private int _ramp_start;
    public int ramp_start
    {
        get { return _ramp_start; }
        set
        {
            _ramp_start = value;
            OnPropertyChanged(nameof(ramp_start));
        }
    }

    private int _ramp_mode;
    public int ramp_mode
    {
        get { return _ramp_mode; }
        set
        {
            _ramp_mode = value;
            OnPropertyChanged(nameof(ramp_mode));
        }
    }

    private Pv _pv;
    public Pv PV
    {
        get { return _pv; }
        set
        {
            _pv = value;
            OnPropertyChanged(nameof(PV));
        }
    }
}

public class O2 : BaseViewModel
{
    private int _sts;
    public int sts
    {
        get { return _sts; }
        set
        {
            _sts = value;
            OnPropertyChanged(nameof(sts));
        }
    }

    private int _flow_sp;
    public int flow_sp
    {
        get { return _flow_sp; }
        set
        {
            _flow_sp = value;
            OnPropertyChanged(nameof(flow_sp));
        }
    }

    private int _flow_pv;
    public int flow_pv
    {
        get { return _flow_pv; }
        set
        {
            _flow_pv = value;
            OnPropertyChanged(nameof(flow_pv));
        }
    }

    private int _vol_pv;
    public int vol_pv
    {
        get { return _vol_pv; }
        set
        {
            _vol_pv = value;
            OnPropertyChanged(nameof(vol_pv));
        }
    }

    private int _alm_enb;
    public int alm_enb
    {
        get { return _alm_enb; }
        set
        {
            _alm_enb = value;
            OnPropertyChanged(nameof(alm_enb));
        }
    }

    private int _alm_sts;
    public int alm_sts
    {
        get { return _alm_sts; }
        set
        {
            _alm_sts = value;
            OnPropertyChanged(nameof(alm_sts));
        }
    }

    private int _alm_llm;
    public int alm_llm
    {
        get { return _alm_llm; }
        set
        {
            _alm_llm = value;
            OnPropertyChanged(nameof(alm_llm));
        }
    }

    private int _alm_ulm;
    public int alm_ulm
    {
        get { return _alm_ulm; }
        set
        {
            _alm_ulm = value;
            OnPropertyChanged(nameof(alm_ulm));
        }
    }

    private ObservableCollection<object> _sp_ramp;
    public ObservableCollection<object> sp_ramp
    {
        get { return _sp_ramp; }
        set
        {
            _sp_ramp = value;
            OnPropertyChanged(nameof(sp_ramp));
        }
    }

    private int _ramp_idx;
    public int ramp_idx
    {
        get { return _ramp_idx; }
        set
        {
            _ramp_idx = value;
            OnPropertyChanged(nameof(ramp_idx));
        }
    }

    private int _ramp_start;
    public int ramp_start
    {
        get { return _ramp_start; }
        set
        {
            _ramp_start = value;
            OnPropertyChanged(nameof(ramp_start));
        }
    }

    private int _ramp_mode;
    public int ramp_mode
    {
        get { return _ramp_mode; }
        set
        {
            _ramp_mode = value;
            OnPropertyChanged(nameof(ramp_mode));
        }
    }

    private Pv _pv;
    public Pv PV
    {
        get { return _pv; }
        set
        {
            _pv = value;
            OnPropertyChanged(nameof(PV));
        }
    }
}

public class PH : BaseViewModel
{
    private int _sts;
    public int sts
    {
        get { return _sts; }
        set
        {
            _sts = value;
            OnPropertyChanged(nameof(sts));
        }
    }

    private int _sp;
    public int sp
    {
        get { return _sp; }
        set
        {
            _sp = value;
            OnPropertyChanged(nameof(sp));
        }
    }

    private int _sp_offset;
    public int sp_offset
    {
        get { return _sp_offset; }
        set
        {
            _sp_offset = value;
            OnPropertyChanged(nameof(sp_offset));
        }
    }

    private int _pv;
    public int pv
    {
        get { return _pv; }
        set
        {
            _pv = value;
            OnPropertyChanged(nameof(pv));
        }
    }

    private int _alm_enb;
    public int alm_enb
    {
        get { return _alm_enb; }
        set
        {
            _alm_enb = value;
            OnPropertyChanged(nameof(alm_enb));
        }
    }

    private int _alm_sts;
    public int alm_sts
    {
        get { return _alm_sts; }
        set
        {
            _alm_sts = value;
            OnPropertyChanged(nameof(alm_sts));
        }
    }

    private int _alm_llm;
    public int alm_llm
    {
        get { return _alm_llm; }
        set
        {
            _alm_llm = value;
            OnPropertyChanged(nameof(alm_llm));
        }
    }

    private int _alm_ulm;
    public int alm_ulm
    {
        get { return _alm_ulm; }
        set
        {
            _alm_ulm = value;
            OnPropertyChanged(nameof(alm_ulm));
        }
    }

    private ObservableCollection<object> _sp_ramp;
    public ObservableCollection<object> sp_ramp
    {
        get { return _sp_ramp; }
        set
        {
            _sp_ramp = value;
            OnPropertyChanged(nameof(sp_ramp));
        }
    }

    private int _ramp_idx;
    public int ramp_idx
    {
        get { return _ramp_idx; }
        set
        {
            _ramp_idx = value;
            OnPropertyChanged(nameof(ramp_idx));
        }
    }

    private int _ramp_start;
    public int ramp_start
    {
        get { return _ramp_start; }
        set
        {
            _ramp_start = value;
            OnPropertyChanged(nameof(ramp_start));
        }
    }

    private int _ramp_mode;
    public int ramp_mode
    {
        get { return _ramp_mode; }
        set
        {
            _ramp_mode = value;
            OnPropertyChanged(nameof(ramp_mode));
        }
    }

    private int _kp;
    public int kp
    {
        get { return _kp; }
        set
        {
            _kp = value;
            OnPropertyChanged(nameof(kp));
        }
    }

    private int _ki;
    public int ki
    {
        get { return _ki; }
        set
        {
            _ki = value;
            OnPropertyChanged(nameof(ki));
        }
    }

    private int _kd;
    public int kd
    {
        get { return _kd; }
        set
        {
            _kd = value;
            OnPropertyChanged(nameof(kd));
        }
    }

    private int _kp_default;
    public int kp_default
    {
        get { return _kp_default; }
        set
        {
            _kp_default = value;
            OnPropertyChanged(nameof(kp_default));
        }
    }

    private int _ki_default;
    public int ki_default
    {
        get { return _ki_default; }
        set
        {
            _ki_default = value;
            OnPropertyChanged(nameof(ki_default));
        }
    }

    private int _kd_default;
    public int kd_default
    {
        get { return _kd_default; }
        set
        {
            _kd_default = value;
            OnPropertyChanged(nameof(kd_default));
        }
    }

    private int _dead_time;
    public int dead_time
    {
        get { return _dead_time; }
        set
        {
            _dead_time = value;
            OnPropertyChanged(nameof(dead_time));
        }
    }

    private int _pulse_time;
    public int pulse_time
    {
        get { return _pulse_time; }
        set
        {
            _pulse_time = value;
            OnPropertyChanged(nameof(pulse_time));
        }
    }

    private int _deadband;
    public int deadband
    {
        get { return _deadband; }
        set
        {
            _deadband = value;
            OnPropertyChanged(nameof(deadband));
        }
    }
}

public class PH_CAL : BaseViewModel
{
    private int _sts;
    public int sts
    {
        get { return _sts; }
        set
        {
            _sts = value;
            OnPropertyChanged(nameof(sts));
        }
    }

    private int _mode;
    public int mode
    {
        get { return _mode; }
        set
        {
            _mode = value;
            OnPropertyChanged(nameof(mode));
        }
    }

    private int _time_stamp;
    public int time_stamp
    {
        get { return _time_stamp; }
        set
        {
            _time_stamp = value;
            OnPropertyChanged(nameof(time_stamp));
        }
    }

    private int _zero;
    public int zero
    {
        get { return _zero; }
        set
        {
            _zero = value;
            OnPropertyChanged(nameof(zero));
        }
    }

    private int _slope;
    public int slope
    {
        get { return _slope; }
        set
        {
            _slope = value;
            OnPropertyChanged(nameof(slope));
        }
    }

    private int _measure;
    public int measure
    {
        get { return _measure; }
        set
        {
            _measure = value;
            OnPropertyChanged(nameof(measure));
        }
    }

    private int _measure_ref;
    public int measure_ref
    {
        get { return _measure_ref; }
        set
        {
            _measure_ref = value;
            OnPropertyChanged(nameof(measure_ref));
        }
    }

    private int _ph_ref1;
    public int ph_ref1
    {
        get { return _ph_ref1; }
        set
        {
            _ph_ref1 = value;
            OnPropertyChanged(nameof(ph_ref1));
        }
    }

    private int _ph_ref2;
    public int ph_ref2
    {
        get { return _ph_ref2; }
        set
        {
            _ph_ref2 = value;
            OnPropertyChanged(nameof(ph_ref2));
        }
    }

    private int _ph_ref3;
    public int ph_ref3
    {
        get { return _ph_ref3; }
        set
        {
            _ph_ref3 = value;
            OnPropertyChanged(nameof(ph_ref3));
        }
    }

    private int _temp_ref;
    public int temp_ref
    {
        get { return _temp_ref; }
        set
        {
            _temp_ref = value;
            OnPropertyChanged(nameof(temp_ref));
        }
    }

    private int _condition;
    public int condition
    {
        get { return _condition; }
        set
        {
            _condition = value;
            OnPropertyChanged(nameof(condition));
        }
    }

    private string _snr;
    public string snr
    {
        get { return _snr; }
        set
        {
            _snr = value;
            OnPropertyChanged(nameof(snr));
        }
    }
}

public class Pres { }

public class Pv { }

public class RECIPE { }

public class RemPv { }

public class SAMPLEDATA : BaseViewModel
{
    private string _label;
    public string label
    {
        get { return _label; }
        set
        {
            _label = value;
            OnPropertyChanged(nameof(label));
        }
    }

    private string _desc;
    public string desc
    {
        get { return _desc; }
        set
        {
            _desc = value;
            OnPropertyChanged(nameof(desc));
        }
    }

    private int _time_stamp;
    public int time_stamp
    {
        get { return _time_stamp; }
        set
        {
            _time_stamp = value;
            OnPropertyChanged(nameof(time_stamp));
        }
    }
}

public class SETVALUEAUTOMATION : BaseViewModel
{
    private int _mode;
    public int mode
    {
        get { return _mode; }
        set
        {
            _mode = value;
            OnPropertyChanged(nameof(mode));
        }
    }

    private int _index;
    public int index
    {
        get { return _index; }
        set
        {
            _index = value;
            OnPropertyChanged(nameof(index));
        }
    }

    private int _cond;
    public int cond
    {
        get { return _cond; }
        set
        {
            _cond = value;
            OnPropertyChanged(nameof(cond));
        }
    }

    private int _ramp;
    public int ramp
    {
        get { return _ramp; }
        set
        {
            _ramp = value;
            OnPropertyChanged(nameof(ramp));
        }
    }

    private ObservableCollection<int> _duration;
    public ObservableCollection<int> duration
    {
        get { return _duration; }
        set
        {
            _duration = value;
            OnPropertyChanged(nameof(duration));
        }
    }

    private ObservableCollection<object> _temp;
    public ObservableCollection<object> temp
    {
        get { return _temp; }
        set
        {
            _temp = value;
            OnPropertyChanged(nameof(temp));
        }
    }

    private ObservableCollection<object> _ph;
    public ObservableCollection<object> ph
    {
        get { return _ph; }
        set
        {
            _ph = value;
            OnPropertyChanged(nameof(ph));
        }
    }

    private ObservableCollection<object> _dos;
    public ObservableCollection<object> dos
    {
        get { return _dos; }
        set
        {
            _dos = value;
            OnPropertyChanged(nameof(dos));
        }
    }

    private ObservableCollection<object> _stirrer;
    public ObservableCollection<object> stirrer
    {
        get { return _stirrer; }
        set
        {
            _stirrer = value;
            OnPropertyChanged(nameof(stirrer));
        }
    }

    private ObservableCollection<object> _air;
    public ObservableCollection<object> air
    {
        get { return _air; }
        set
        {
            _air = value;
            OnPropertyChanged(nameof(air));
        }
    }

    private ObservableCollection<object> _o2;
    public ObservableCollection<object> o2
    {
        get { return _o2; }
        set
        {
            _o2 = value;
            OnPropertyChanged(nameof(o2));
        }
    }

    private ObservableCollection<object> _n2;
    public ObservableCollection<object> n2
    {
        get { return _n2; }
        set
        {
            _n2 = value;
            OnPropertyChanged(nameof(n2));
        }
    }

    private ObservableCollection<object> _co2;
    public ObservableCollection<object> co2
    {
        get { return _co2; }
        set
        {
            _co2 = value;
            OnPropertyChanged(nameof(co2));
        }
    }

    private ObservableCollection<object> _acid;
    public ObservableCollection<object> acid
    {
        get { return _acid; }
        set
        {
            _acid = value;
            OnPropertyChanged(nameof(acid));
        }
    }

    private ObservableCollection<object> _bases;
    public ObservableCollection<object> bases
    {
        get { return _bases; }
        set
        {
            _bases = value;
            OnPropertyChanged(nameof(bases));
        }
    }

    private ObservableCollection<object> _afoam;
    public ObservableCollection<object> afoam
    {
        get { return _afoam; }
        set
        {
            _afoam = value;
            OnPropertyChanged(nameof(afoam));
        }
    }

    private ObservableCollection<object> _level;
    public ObservableCollection<object> level
    {
        get { return _level; }
        set
        {
            _level = value;
            OnPropertyChanged(nameof(level));
        }
    }

    private ObservableCollection<object> _suba;
    public ObservableCollection<object> suba
    {
        get { return _suba; }
        set
        {
            _suba = value;
            OnPropertyChanged(nameof(suba));
        }
    }
}

public class SpRamp : BaseViewModel
{
    private int _sp;
    public int sp
    {
        get { return _sp; }
        set
        {
            _sp = value;
            OnPropertyChanged(nameof(sp));
        }
    }

    private int _duration;
    public int duration
    {
        get { return _duration; }
        set
        {
            _duration = value;
            OnPropertyChanged(nameof(duration));
        }
    }
}

public class STIRRER : BaseViewModel
{
    private int _sts;
    public int sts
    {
        get { return _sts; }
        set
        {
            _sts = value;
            OnPropertyChanged(nameof(sts));
        }
    }

    private int _dir;
    public int dir
    {
        get { return _dir; }
        set
        {
            _dir = value;
            OnPropertyChanged(nameof(dir));
        }
    }

    private int _sp;
    public int sp
    {
        get { return _sp; }
        set
        {
            _sp = value;
            OnPropertyChanged(nameof(sp));
        }
    }

    private int _sp_min;
    public int sp_min
    {
        get { return _sp_min; }
        set
        {
            _sp_min = value;
            OnPropertyChanged(nameof(sp_min));
        }
    }

    private int _sp_max;
    public int sp_max
    {
        get { return _sp_max; }
        set
        {
            _sp_max = value;
            OnPropertyChanged(nameof(sp_max));
        }
    }

    private int _pv;
    public int pv
    {
        get { return _pv; }
        set
        {
            _pv = value;
            OnPropertyChanged(nameof(pv));
        }
    }

    private int _alm_enb;
    public int alm_enb
    {
        get { return _alm_enb; }
        set
        {
            _alm_enb = value;
            OnPropertyChanged(nameof(alm_enb));
        }
    }

    private int _alm_sts;
    public int alm_sts
    {
        get { return _alm_sts; }
        set
        {
            _alm_sts = value;
            OnPropertyChanged(nameof(alm_sts));
        }
    }

    private int _alm_llm;
    public int alm_llm
    {
        get { return _alm_llm; }
        set
        {
            _alm_llm = value;
            OnPropertyChanged(nameof(alm_llm));
        }
    }

    private int _alm_ulm;
    public int alm_ulm
    {
        get { return _alm_ulm; }
        set
        {
            _alm_ulm = value;
            OnPropertyChanged(nameof(alm_ulm));
        }
    }

    private ObservableCollection<object> _sp_ramp;
    public ObservableCollection<object> sp_ramp
    {
        get { return _sp_ramp; }
        set
        {
            _sp_ramp = value;
            OnPropertyChanged(nameof(sp_ramp));
        }
    }

    private int _ramp_idx;
    public int ramp_idx
    {
        get { return _ramp_idx; }
        set
        {
            _ramp_idx = value;
            OnPropertyChanged(nameof(ramp_idx));
        }
    }

    private int _ramp_start;
    public int ramp_start
    {
        get { return _ramp_start; }
        set
        {
            _ramp_start = value;
            OnPropertyChanged(nameof(ramp_start));
        }
    }

    private int _ramp_mode;
    public int ramp_mode
    {
        get { return _ramp_mode; }
        set
        {
            _ramp_mode = value;
            OnPropertyChanged(nameof(ramp_mode));
        }
    }
}

public class Sts { }

public class SUBA : BaseViewModel
{
    private int _sts;
    public int sts
    {
        get { return _sts; }
        set
        {
            _sts = value;
            OnPropertyChanged(nameof(sts));
        }
    }

    private int _dir;
    public int dir
    {
        get { return _dir; }
        set
        {
            _dir = value;
            OnPropertyChanged(nameof(dir));
        }
    }

    private int _act_dev;
    public int act_dev
    {
        get { return _act_dev; }
        set
        {
            _act_dev = value;
            OnPropertyChanged(nameof(act_dev));
        }
    }

    private int _vol_sp;
    public int vol_sp
    {
        get { return _vol_sp; }
        set
        {
            _vol_sp = value;
            OnPropertyChanged(nameof(vol_sp));
        }
    }

    private int _vol_pv;
    public int vol_pv
    {
        get { return _vol_pv; }
        set
        {
            _vol_pv = value;
            OnPropertyChanged(nameof(vol_pv));
        }
    }

    private int _flow_sp;
    public int flow_sp
    {
        get { return _flow_sp; }
        set
        {
            _flow_sp = value;
            OnPropertyChanged(nameof(flow_sp));
        }
    }

    private int _flow_pv;
    public int flow_pv
    {
        get { return _flow_pv; }
        set
        {
            _flow_pv = value;
            OnPropertyChanged(nameof(flow_pv));
        }
    }

    private int _alm_enb;
    public int alm_enb
    {
        get { return _alm_enb; }
        set
        {
            _alm_enb = value;
            OnPropertyChanged(nameof(alm_enb));
        }
    }

    private int _alm_sts;
    public int alm_sts
    {
        get { return _alm_sts; }
        set
        {
            _alm_sts = value;
            OnPropertyChanged(nameof(alm_sts));
        }
    }

    private int _alm_llm;
    public int alm_llm
    {
        get { return _alm_llm; }
        set
        {
            _alm_llm = value;
            OnPropertyChanged(nameof(alm_llm));
        }
    }

    private int _alm_ulm;
    public int alm_ulm
    {
        get { return _alm_ulm; }
        set
        {
            _alm_ulm = value;
            OnPropertyChanged(nameof(alm_ulm));
        }
    }

    private ObservableCollection<object> _sp_ramp;
    public ObservableCollection<object> sp_ramp
    {
        get { return _sp_ramp; }
        set
        {
            _sp_ramp = value;
            OnPropertyChanged(nameof(sp_ramp));
        }
    }

    private int _ramp_idx;
    public int ramp_idx
    {
        get { return _ramp_idx; }
        set
        {
            _ramp_idx = value;
            OnPropertyChanged(nameof(ramp_idx));
        }
    }

    private int _ramp_start;
    public int ramp_start
    {
        get { return _ramp_start; }
        set
        {
            _ramp_start = value;
            OnPropertyChanged(nameof(ramp_start));
        }
    }

    private int _ramp_mode;
    public int ramp_mode
    {
        get { return _ramp_mode; }
        set
        {
            _ramp_mode = value;
            OnPropertyChanged(nameof(ramp_mode));
        }
    }

    private int _rem_pv;
    public int rem_pv
    {
        get { return _rem_pv; }
        set
        {
            _rem_pv = value;
            OnPropertyChanged(nameof(rem_pv));
        }
    }
}

public class SUBA_CAL : BaseViewModel
{
    private int _sts;
    public int sts
    {
        get { return _sts; }
        set
        {
            _sts = value;
            OnPropertyChanged(nameof(sts));
        }
    }

    private int _time_stamp;
    public int time_stamp
    {
        get { return _time_stamp; }
        set
        {
            _time_stamp = value;
            OnPropertyChanged(nameof(time_stamp));
        }
    }

    private int _slope;
    public int slope
    {
        get { return _slope; }
        set
        {
            _slope = value;
            OnPropertyChanged(nameof(slope));
        }
    }

    private int _tubing;
    public int tubing
    {
        get { return _tubing; }
        set
        {
            _tubing = value;
            OnPropertyChanged(nameof(tubing));
        }
    }

    private int _vol_sp;
    public int vol_sp
    {
        get { return _vol_sp; }
        set
        {
            _vol_sp = value;
            OnPropertyChanged(nameof(vol_sp));
        }
    }

    private int _measure;
    public int measure
    {
        get { return _measure; }
        set
        {
            _measure = value;
            OnPropertyChanged(nameof(measure));
        }
    }

    private int _tube_len;
    public int tube_len
    {
        get { return _tube_len; }
        set
        {
            _tube_len = value;
            OnPropertyChanged(nameof(tube_len));
        }
    }
}

public class SUBS
{
    public int sts { get; set; }
}

public class SysColor { }

/*[ObservableObject]
public partial class SYSTEM
{
    [ObservableProperty] public int _sts;
    [ObservableProperty] public string _dev_name;
    [ObservableProperty] public int _dev_type;
    [ObservableProperty] public string _host_name;
    [ObservableProperty] public int _port_name;
    [ObservableProperty] public string _snr;
    [ObservableProperty] public string _pname;
    [ObservableProperty] public string _pid;
    [ObservableProperty] public string _fw_ver;
    [ObservableProperty] public string _fw_build;
    [ObservableProperty] public string _gui_ver;
    [ObservableProperty] public string _gui_build;
    [ObservableProperty] public int _unixtime;
    [ObservableProperty] public string _tz;
    [ObservableProperty] public int _auto_restart;
    [ObservableProperty] public int _sys_color;
    [ObservableProperty] public int _alm_sts;
    [ObservableProperty] public int _alm_sound;
    [ObservableProperty] public int _cfr_enb;
    [ObservableProperty] public int _use_bluevary;
    [ObservableProperty] public int _use_cgqbior;
}*/

public class SYSTEM : BaseViewModel
{
    private int _sts;
    public int sts
    {
        get { return _sts; }
        set
        {
            _sts = value;
            OnPropertyChanged(nameof(sts));
        }
    }

    private string _dev_name;
    public string dev_name
    {
        get { return _dev_name; }
        set
        {
            _dev_name = value;
            OnPropertyChanged(nameof(dev_name));
        }
    }

    private int _dev_type;
    public int dev_type
    {
        get { return _dev_type; }
        set
        {
            _dev_type = value;
            OnPropertyChanged(nameof(dev_type));
        }
    }

    private string _host_name;
    public string host_name
    {
        get { return _host_name; }
        set
        {
            _host_name = value;
            OnPropertyChanged(nameof(host_name));
        }
    }

    private int _port_name;
    public int port_name
    {
        get { return _port_name; }
        set
        {
            _port_name = value;
            OnPropertyChanged(nameof(port_name));
        }
    }

    private string _snr;
    public string snr
    {
        get { return _snr; }
        set
        {
            _snr = value;
            OnPropertyChanged(nameof(snr));
        }
    }

    private string _pname;
    public string pname
    {
        get { return _pname; }
        set
        {
            _pname = value;
            OnPropertyChanged(nameof(pname)); ;
        }
    }

    private string _pid;
    public string pid
    {
        get { return _pid; }
        set
        {
            _pid = value;
            OnPropertyChanged(nameof(pid));
        }
    }

    private string _fw_ver;
    public string fw_ver
    {
        get { return _fw_ver; }
        set
        {
            _fw_ver = value;
            OnPropertyChanged(nameof(fw_ver));
        }
    }

    private string _fw_build;
    public string fw_build
    {
        get { return _fw_build; }
        set
        {
            _fw_build = value;
            OnPropertyChanged(nameof(fw_build));
        }
    }

    private string _gui_ver;
    public string gui_ver
    {
        get { return _gui_ver; }
        set
        {
            _gui_ver = value;
            OnPropertyChanged(nameof(gui_ver));
        }
    }

    private string _gui_build;
    public string gui_build
    {
        get { return _gui_build; }
        set
        {
            _gui_build = value;
            OnPropertyChanged(nameof(gui_build));
        }
    }

    private int _unixtime;
    public int unixtime
    {
        get { return _unixtime; }
        set
        {
            _unixtime = value;
            OnPropertyChanged(nameof(unixtime));
        }
    }

    private string _tz;
    public string tz
    {
        get { return _tz; }
        set
        {
            _tz = value;
            OnPropertyChanged(nameof(tz));
        }
    }

    private int _auto_restart;
    public int auto_restart
    {
        get { return _auto_restart; }
        set
        {
            _auto_restart = value;
            OnPropertyChanged(nameof(auto_restart));
        }
    }

    private int _sys_color;
    public int sys_color
    {
        get { return _sys_color; }
        set
        {
            _sys_color = value;
            OnPropertyChanged(nameof(sys_color));
        }
    }

    private int _alm_sts;
    public int alm_sts
    {
        get { return _alm_sts; }
        set
        {
            _alm_sts = value;
            OnPropertyChanged(nameof(alm_sts));
        }
    }

    private int _alm_sound;
    public int alm_sound
    {
        get { return _alm_sound; }
        set
        {
            _alm_sound = value;
            OnPropertyChanged(nameof(alm_sound));
        }
    }

    private int _cfr_enb;
    public int cfr_enb
    {
        get { return _cfr_enb; }
        set
        {
            _cfr_enb = value;
            OnPropertyChanged(nameof(cfr_enb));
        }
    }

    private int _use_bluevary;
    public int use_bluevary
    {
        get { return _use_bluevary; }
        set
        {
            _use_bluevary = value;
            OnPropertyChanged(nameof(use_bluevary));
        }
    }

    private int _use_cgqbior;
    public int cgqbior
    {
        get { return _use_cgqbior; }
        set
        {
            _use_cgqbior = value;
            OnPropertyChanged(nameof(cgqbior));
        }
    }

    private int _pel_drv;
    public int pel_drv
    {
        get { return _pel_drv; }
        set
        {
            _pel_drv = value;
            OnPropertyChanged(nameof(pel_drv));
        }
    }

    private int _pump_drv;
    public int pump_drv
    {
        get { return _pump_drv; }
        set
        {
            _pump_drv = value;
            OnPropertyChanged(nameof(pump_drv));
        }
    }

    private int _mfc_ver;
    public int mfc_ver
    {
        get { return _mfc_ver; }
        set
        {
            _mfc_ver = value;
            OnPropertyChanged(nameof(mfc_ver));
        }
    }
}

public class USERLOGIN
{
    public string name { get; set; }
    public string hash { get; set; }
}

public class TEMP : BaseViewModel
{
    private int _sts;
    public int sts
    {
        get { return _sts; }
        set
        {
            _sts = value;
            OnPropertyChanged(nameof(sts));
        }
    }

    private int _sp;
    public int sp
    {
        get { return _sp; }
        set
        {
            _sp = value;
            OnPropertyChanged(nameof(sp));
        }
    }

    private int _sp_offset;
    public int sp_offset
    {
        get { return _sp_offset; }
        set
        {
            _sp_offset = value;
            OnPropertyChanged(nameof(sp_offset));
        }
    }

    private int _pv;
    public int pv
    {
        get { return _pv; }
        set
        {
            _pv = value;
            OnPropertyChanged(nameof(pv));
        }
    }

    private int _act_dev;
    public int act_dev
    {
        get { return _act_dev; }
        set
        {
            _act_dev = value;
            OnPropertyChanged(nameof(act_dev));
        }
    }

    private int _htr_sp;
    public int htr_sp
    {
        get { return _htr_sp; }
        set
        {
            _htr_sp = value;
            OnPropertyChanged(nameof(htr_sp));
        }
    }

    private int _htr_pv;
    public int htr_pv
    {
        get { return _htr_pv; }
        set
        {
            _htr_pv = value;
            OnPropertyChanged(nameof(htr_pv));
        }
    }

    private int _chw_sp;
    public int chw_sp
    {
        get { return _chw_sp; }
        set
        {
            _chw_sp = value;
            OnPropertyChanged(nameof(chw_sp));
        }
    }

    private int _chw_pv;
    public int chw_pv
    {
        get { return _chw_pv; }
        set
        {
            _chw_pv = value;
            OnPropertyChanged(nameof(chw_pv));
        }
    }

    private int _alm_enb;
    public int alm_enb
    {
        get { return _alm_enb; }
        set
        {
            _alm_enb = value;
            OnPropertyChanged(nameof(alm_enb));
        }
    }

    private int _alm_sts;
    public int alm_sts
    {
        get { return _alm_sts; }
        set
        {
            _alm_sts = value;
            OnPropertyChanged(nameof(alm_sts));
        }
    }

    private int _htr_alm_sts;
    public int htr_alm_sts
    {
        get { return _htr_alm_sts; }
        set
        {
            _htr_alm_sts = value;
            OnPropertyChanged(nameof(htr_alm_sts));
        }
    }


    private int _alm_llm;
    public int alm_llm
    {
        get { return _alm_llm; }
        set
        {
            _alm_llm = value;
            OnPropertyChanged(nameof(alm_llm));
        }
    }

    private int _alm_ulm;
    public int alm_ulm
    {
        get { return _alm_ulm; }
        set
        {
            _alm_ulm = value;
            OnPropertyChanged(nameof(alm_ulm));
        }
    }

    private ObservableCollection<object> _sp_ramp;
    public ObservableCollection<object> sp_ramp
    {
        get { return _sp_ramp; }
        set
        {
            _sp_ramp = value;
            OnPropertyChanged(nameof(sp_ramp));
        }
    }

    private int _ramp_idx;
    public int ramp_idx
    {
        get { return _ramp_idx; }
        set
        {
            _ramp_idx = value;
            OnPropertyChanged(nameof(ramp_idx));
        }
    }

    private int _ramp_start;
    public int ramp_start
    {
        get { return _ramp_start; }
        set
        {
            _ramp_start = value;
            OnPropertyChanged(nameof(ramp_start));
        }
    }

    private int _ramp_mode;
    public int ramp_mode
    {
        get { return _ramp_mode; }
        set
        {
            _ramp_mode = value;
            OnPropertyChanged(nameof(ramp_mode));
        }
    }

    private int _kp;
    public int kp
    {
        get { return _kp; }
        set
        {
            _kp = value;
            OnPropertyChanged(nameof(kp));
        }
    }

    private int _ki;
    public int ki
    {
        get { return _ki; }
        set
        {
            _ki = value;
            OnPropertyChanged(nameof(ki));
        }
    }

    private int _kd;
    public int kd
    {
        get { return _kd; }
        set
        {
            _kd = value;
            OnPropertyChanged(nameof(kd));
        }
    }

    private int _kp_default;
    public int kp_default
    {
        get { return _kp_default; }
        set
        {
            _kp_default = value;
            OnPropertyChanged(nameof(kp_default));
        }
    }

    private int _ki_default;
    public int ki_default
    {
        get { return _ki_default; }
        set
        {
            _ki_default = value;
            OnPropertyChanged(nameof(ki_default));
        }
    }

    private int _kd_default;
    public int kd_default
    {
        get { return _kd_default; }
        set
        {
            _kd_default = value;
            OnPropertyChanged(nameof(kd_default));
        }
    }

    private int _deadband;
    public int deadband
    {
        get { return _deadband; }
        set
        {
            _deadband = value;
            OnPropertyChanged(nameof(deadband));
        }
    }

    private int _coolfinger;
    public int Coolfinger
    {
        get { return _coolfinger; }
        set
        {
            _coolfinger = value;
            OnPropertyChanged(nameof(Coolfinger));
        }
    }
}

public class TEMP_CAL : BaseViewModel
{
    private int _sts;
    public int sts
    {
        get { return _sts; }
        set
        {
            _sts = value;
            OnPropertyChanged(nameof(sts));
        }
    }

    private int _time_stamp;
    public int time_stamp
    {
        get { return _time_stamp; }
        set
        {
            _time_stamp = value;
            OnPropertyChanged(nameof(time_stamp));
        }
    }

    private int _zero;
    public int zero
    {
        get { return _zero; }
        set
        {
            _zero = value;
            OnPropertyChanged(nameof(zero));
        }
    }

    private int _slope;
    public int slope
    {
        get { return _slope; }
        set
        {
            _slope = value;
            OnPropertyChanged(nameof(slope));
        }
    }

    private int _measure_1st;
    public int measure_1st
    {
        get { return _measure_1st; }
        set
        {
            _measure_1st = value;
            OnPropertyChanged(nameof(measure_1st));
        }
    }

    private int _measure_2nd;
    public int measure_2nd
    {
        get { return _measure_2nd; }
        set
        {
            _measure_2nd = value;
            OnPropertyChanged(nameof(measure_2nd));
        }
    }

    private int _condition;
    public int condition
    {
        get { return _condition; }
        set
        {
            _condition = value;
            OnPropertyChanged(nameof(condition));
        }
    }

    private string _snr;
    public string snr
    {
        get { return _snr; }
        set
        {
            _snr = value;
            OnPropertyChanged(nameof(snr));
        }
    }
}

public class TRENDBUFFER : BaseViewModel
{
    private Ch1 _ch1;
    public Ch1 ch1
    {
        get { return _ch1; }
        set
        {
            _ch1 = value;
            OnPropertyChanged(nameof(ch1));
        }
    }

    private Ch2 _ch2;
    public Ch2 ch2
    {
        get { return _ch2; }
        set
        {
            _ch2 = value;
            OnPropertyChanged(nameof(ch2));
        }
    }

    private Ch3 _ch3;
    public Ch3 ch3
    {
        get { return _ch3; }
        set
        {
            _ch3 = value;
            OnPropertyChanged(nameof(ch3));
        }
    }

    private Ch4 _ch4;
    public Ch4 ch4
    {
        get { return _ch4; }
        set
        {
            _ch4 = value;
            OnPropertyChanged(nameof(ch4));
        }
    }

    private Ch5 _ch5;
    public Ch5 ch5
    {
        get { return _ch5; }
        set
        {
            _ch5 = value;
            OnPropertyChanged(nameof(ch5));
        }
    }

    private Ch6 _ch6;
    public Ch6 ch6
    {
        get { return _ch6; }
        set
        {
            _ch6 = value;
            OnPropertyChanged(nameof(ch6));
        }
    }
}

public class TURBIDITY_PROBE : BaseViewModel
{
    private int _sts;
    public int sts
    {
        get { return _sts; }
        set
        {
            _sts = value;
            OnPropertyChanged(nameof(sts));
        }
    }

    private int _pv;
    public int pv
    {
        get { return _pv; }
        set
        {
            _pv = value;
            OnPropertyChanged(nameof(pv));
        }
    }

    private int _alm_enb;
    public int alm_enb
    {
        get { return _alm_enb; }
        set
        {
            _alm_enb = value;
            OnPropertyChanged(nameof(alm_enb));
        }
    }

    private int _alm_sts;
    public int alm_sts
    {
        get { return _alm_sts; }
        set
        {
            _alm_sts = value;
            OnPropertyChanged(nameof(alm_sts));
        }
    }

    private int _alm_llm;
    public int alm_llm
    {
        get { return _alm_llm; }
        set
        {
            _alm_llm = value;
            OnPropertyChanged(nameof(alm_llm));
        }
    }

    private int _alm_ulm;
    public int alm_ulm
    {
        get { return _alm_ulm; }
        set
        {
            _alm_ulm = value;
            OnPropertyChanged(nameof(alm_ulm));
        }
    }

    private int _sp_min;
    public int sp_min
    {
        get { return _sp_min; }
        set
        {
            _sp_min = value;
            OnPropertyChanged(nameof(sp_min));
        }
    }

    private int _sp_max;
    public int sp_max
    {
        get { return _sp_max; }
        set
        {
            _sp_max = value;
            OnPropertyChanged(nameof(sp_max));
        }
    }

    private int _sp_offset;
    public int sp_offset
    {
        get { return _sp_offset; }
        set
        {
            _sp_offset = value;
            OnPropertyChanged(nameof(sp_offset));
        }
    }

    private int _unit;
    public int unit
    {
        get { return _unit; }
        set
        {
            _unit = value;
            OnPropertyChanged(nameof(unit));
        }
    }
}

public class USERLOGOUT { }

public class VolPv { }

public class EXCELL : BaseViewModel
{
    private int _unit;
    public int unit
    {
        get { return _unit; }
        set
        {
            _unit = value;
            OnPropertyChanged(nameof(unit));
        }
    }

    private int _alm_sts;
    public int alm_sts
    {
        get { return _alm_sts; }
        set
        {
            _alm_sts = value;
            OnPropertyChanged(nameof(alm_sts));
        }
    }

    private int _m1_val;
    public int m1_val
    {
        get { return _m1_val; }
        set
        {
            _m1_val = value;
            OnPropertyChanged(nameof(m1_val));
        }
    }

    private int _id;
    public int id
    {
        get { return _id; }
        set
        {
            _id = value;
            OnPropertyChanged(nameof(id));
        }
    }

    private int _m2_val;
    public int m2_val
    {
        get { return _m2_val; }
        set
        {
            _m2_val = value;
            OnPropertyChanged(nameof(m2_val));
        }
    }
}

public class REDOX : BaseViewModel
{
    private int _alm_sts;
    public int alm_sts
    {
        get { return _alm_sts; }
        set
        {
            _alm_sts = value;
            OnPropertyChanged(nameof(alm_sts));
        }
    }

    private int _m1_val;
    public int m1_val
    {
        get { return _m1_val; }
        set
        {
            _m1_val = value;
            OnPropertyChanged(nameof(m1_val));
        }
    }

    private int _id;
    public int id
    {
        get { return _id; }
        set
        {
            _id = value;
            OnPropertyChanged(nameof(id));
        }
    }
}
#pragma warning enable format
