﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace vox2c2s
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length > 0)
            {
                string[] rawvox = File.ReadAllLines(args[0]);
                List<string> laserL_data = new List<string>();
                List<string> laserR_data = new List<string>();
                List<string> bt1_data = new List<string>();
                List<string> bt2_data = new List<string>();
                List<string> bt3_data = new List<string>();
                List<string> bt4_data = new List<string>();
                List<string> fx_L_data = new List<string>();
                List<string> fx_R_data = new List<string>();
                List<string> bpm_data = new List<string>();
                List<string> beat_data = new List<string>();
                List<string> c2s_data = new List<string>();
                for (int i = 0; i < rawvox.Length; i++)
                {
                    if (rawvox[i] == "#BEAT INFO")
                    {
                        for (int k = i; ; k++)
                        {
                            beat_data.Add(rawvox[k]);
                            if (rawvox[k] == "#END") break;
                        }
                        beat_data.RemoveAt(0);
                        beat_data.RemoveAt(beat_data.Count - 1);
                    }

                    if (rawvox[i] == "#BPM INFO")
                    {
                        for (int k = i; ; k++)
                        {
                            bpm_data.Add(rawvox[k]);
                            if (rawvox[k] == "#END") break;
                        }
                        bpm_data.RemoveAt(0);
                        bpm_data.RemoveAt(bpm_data.Count - 1);
                    }

                    if (rawvox[i] == "#TRACK1")
                    {
                        for (int k = i; ; k++)
                        {
                            laserL_data.Add(rawvox[k]);
                            if (rawvox[k] == "#END") break;
                        }
                        laserL_data.RemoveAt(0);
                        laserL_data.RemoveAt(laserL_data.Count - 1);
                    }
                    if (rawvox[i] == "#TRACK2")
                    {
                        for (int k = i; ; k++)
                        {
                            fx_L_data.Add(rawvox[k]);
                            if (rawvox[k] == "#END") break;
                        }
                        fx_L_data.RemoveAt(0);
                        fx_L_data.RemoveAt(fx_L_data.Count - 1);
                    }
                    if (rawvox[i] == "#TRACK3")
                    {
                        for (int k = i; ; k++)
                        {
                            bt1_data.Add(rawvox[k]);
                            if (rawvox[k] == "#END") break;
                        }
                        bt1_data.RemoveAt(0);
                        bt1_data.RemoveAt(bt1_data.Count - 1);
                    }
                    if (rawvox[i] == "#TRACK4")
                    {
                        for (int k = i; ; k++)
                        {
                            bt2_data.Add(rawvox[k]);
                            if (rawvox[k] == "#END") break;
                        }
                        bt2_data.RemoveAt(0);
                        bt2_data.RemoveAt(bt2_data.Count - 1);
                    }
                    if (rawvox[i] == "#TRACK5")
                    {
                        for (int k = i; ; k++)
                        {
                            bt3_data.Add(rawvox[k]);
                            if (rawvox[k] == "#END") break;
                        }
                        bt3_data.RemoveAt(0);
                        bt3_data.RemoveAt(bt3_data.Count - 1);
                    }
                    if (rawvox[i] == "#TRACK6")
                    {
                        for (int k = i; ; k++)
                        {
                            bt4_data.Add(rawvox[k]);
                            if (rawvox[k] == "#END") break;
                        }
                        bt4_data.RemoveAt(0);
                        bt4_data.RemoveAt(bt4_data.Count - 1);
                    }
                    if (rawvox[i] == "#TRACK7")
                    {
                        for (int k = i; ; k++)
                        {
                            fx_R_data.Add(rawvox[k]);
                            if (rawvox[k] == "#END") break;
                        }
                        fx_R_data.RemoveAt(0);
                        fx_R_data.RemoveAt(fx_R_data.Count - 1);
                    }
                    if (rawvox[i] == "#TRACK8")
                    {
                        for (int k = i; ; k++)
                        {
                            laserR_data.Add(rawvox[k]);
                            if (rawvox[k] == "#END") break;
                        }
                        laserR_data.RemoveAt(0);
                        laserR_data.RemoveAt(laserR_data.Count - 1);
                    }
                }

                foreach (string line in bpm_data)
                {
                    string[] sp = line.Split('\t');
                    string[] pos = sp[0].Split(',');
                    //double bpm = double.Parse(sp[1]);
                    int beat = int.Parse(pos[0]);
                    int bar = int.Parse(pos[1]);
                    int tick = int.Parse(pos[2]);
                    int offset = ((bar - 1) * 48) + tick;
                    c2s_data.Add(string.Format("BPM\t{0}\t{1}\t{2}", beat - 1, offset * 2, sp[1]));
                }
                foreach (string line in beat_data)
                {
                    string[] sp = line.Split('\t');
                    string[] pos = sp[0].Split(',');
                    int beat = int.Parse(pos[0]);
                    int bar = int.Parse(pos[1]);
                    int tick = int.Parse(pos[2]);
                    int offset = ((bar - 1) * 48) + tick;
                    c2s_data.Add(string.Format("MET\t{0}\t{1}\t{2}\t{3}", beat - 1, offset * 2, sp[2], sp[1]));
                }
                foreach (string line in bt1_data)
                {
                    string[] sp = line.Split('\t');
                    string[] pos = sp[0].Split(',');
                    int beat = int.Parse(pos[0]);
                    int bar = int.Parse(pos[1]);
                    int tick = int.Parse(pos[2]);
                    int offset = ((bar - 1) * 48) + tick;
                    int long_length = int.Parse(sp[1]);
                    if (long_length <= 0)
                    {
                        c2s_data.Add(string.Format("TAP\t{0}\t{1}\t0\t4", beat - 1, offset * 2));
                    }
                    else
                    {
                        c2s_data.Add(string.Format("HLD\t{0}\t{1}\t0\t4\t{2}", beat - 1, offset * 2, long_length * 2));
                    }
                }
                foreach (string line in bt2_data)
                {
                    string[] sp = line.Split('\t');
                    string[] pos = sp[0].Split(',');
                    int beat = int.Parse(pos[0]);
                    int bar = int.Parse(pos[1]);
                    int tick = int.Parse(pos[2]);
                    int offset = ((bar - 1) * 48) + tick;
                    int long_length = int.Parse(sp[1]);
                    if (long_length <= 0)
                    {
                        c2s_data.Add(string.Format("TAP\t{0}\t{1}\t4\t4", beat - 1, offset * 2));
                    }
                    else
                    {
                        c2s_data.Add(string.Format("HLD\t{0}\t{1}\t4\t4\t{2}", beat - 1, offset * 2, long_length * 2));
                    }
                }
                foreach (string line in bt3_data)
                {
                    string[] sp = line.Split('\t');
                    string[] pos = sp[0].Split(',');
                    int beat = int.Parse(pos[0]);
                    int bar = int.Parse(pos[1]);
                    int tick = int.Parse(pos[2]);
                    int offset = ((bar - 1) * 48) + tick;
                    int long_length = int.Parse(sp[1]);
                    if (long_length <= 0)
                    {
                        c2s_data.Add(string.Format("TAP\t{0}\t{1}\t8\t4", beat - 1, offset * 2));
                    }
                    else
                    {
                        c2s_data.Add(string.Format("HLD\t{0}\t{1}\t8\t4\t{2}", beat - 1, offset * 2, long_length * 2));
                    }
                }
                foreach (string line in bt4_data)
                {
                    string[] sp = line.Split('\t');
                    string[] pos = sp[0].Split(',');
                    int beat = int.Parse(pos[0]);
                    int bar = int.Parse(pos[1]);
                    int tick = int.Parse(pos[2]);
                    int offset = ((bar - 1) * 48) + tick;
                    int long_length = int.Parse(sp[1]);
                    if (long_length <= 0)
                    {
                        c2s_data.Add(string.Format("TAP\t{0}\t{1}\t12\t4", beat - 1, offset * 2));
                    }
                    else
                    {
                        c2s_data.Add(string.Format("HLD\t{0}\t{1}\t12\t4\t{2}", beat - 1, offset * 2, long_length * 2));
                    }
                }
                foreach (string line in fx_L_data)
                {
                    string[] sp = line.Split('\t');
                    string[] pos = sp[0].Split(',');
                    int beat = int.Parse(pos[0]);
                    int bar = int.Parse(pos[1]);
                    int tick = int.Parse(pos[2]);
                    int offset = ((bar - 1) * 48) + tick;
                    int long_length = int.Parse(sp[1]);
                    if (long_length <= 0)
                    {
                        c2s_data.Add(string.Format("CHR\t{0}\t{1}\t0\t8", beat - 1, offset * 2));
                    }
                    else
                    {
                        c2s_data.Add(string.Format("CHR\t{0}\t{1}\t0\t8", beat - 1, offset * 2));
                        c2s_data.Add(string.Format("HLD\t{0}\t{1}\t0\t8\t{2}", beat - 1, offset * 2, long_length * 2));
                        //c2s_data.Add(string.Format("CHR\t{0}\t{1}\t0\t8", beat - 1, (offset * 2) + (long_length * 2)));
                    }
                }
                foreach (string line in fx_R_data)
                {
                    string[] sp = line.Split('\t');
                    string[] pos = sp[0].Split(',');
                    int beat = int.Parse(pos[0]);
                    int bar = int.Parse(pos[1]);
                    int tick = int.Parse(pos[2]);
                    int offset = ((bar - 1) * 48) + tick;
                    int long_length = int.Parse(sp[1]);
                    if (long_length <= 0)
                    {
                        c2s_data.Add(string.Format("CHR\t{0}\t{1}\t8\t8", beat - 1, offset * 2));
                    }
                    else
                    {
                        c2s_data.Add(string.Format("CHR\t{0}\t{1}\t8\t8", beat - 1, offset * 2));
                        c2s_data.Add(string.Format("HLD\t{0}\t{1}\t8\t8\t{2}", beat - 1, offset * 2, long_length * 2));
                        //c2s_data.Add(string.Format("CHR\t{0}\t{1}\t8\t8", beat - 1, (offset * 2)+ (long_length * 2)));
                    }
                }
                

                bool laser_L = false;
                int laserL_start = 0;
                int laser_L_count = 0;
                bool laser_R = false;
                for (int i = 0; i < laserL_data.Count; i++)
                {
                    string[] sp = laserL_data[i].Split('\t');
                    string[] pos = sp[0].Split(',');

                    int laserconverted = laser_conv(int.Parse(sp[1]));
                    int beat = int.Parse(pos[0]);
                    int bar = int.Parse(pos[1]);
                    int tick = int.Parse(pos[2]);
                    int offset = ((bar - 1) * 48) + tick;
                    int long_length = 0;


                    if (i < laserL_data.Count - 1)
                    {
                        laser_L = true;
                        if (sp[2] != "2" && laserL_data[i + 1].Split('\t')[2] != "2")
                        {
                            string[] sp_2 = laserL_data[i + 1].Split('\t');
                            string[] pos_2 = sp_2[0].Split(',');
                            int laserconverted_end = laser_conv(int.Parse(sp_2[1]));
                            int beat_1 = int.Parse(pos_2[0]);
                            int bar_1 = int.Parse(pos_2[1]);
                            int tick_1 = int.Parse(pos_2[2]);
                            long_length = (((beat_1 - beat) * 192) + ((bar_1 - bar) * 48) + (tick_1 - tick)) * 2;
                            if (long_length == 0)
                            {
                                int flk_size = 0;
                                flk_size = laserconverted + laserconverted_end + 4;
                                if (laserconverted > laserconverted_end)
                                {
                                    c2s_data.Add(string.Format("FLK\t{0}\t{1}\t{2}\t{3}", beat - 1, offset * 2, laserconverted_end, flk_size));
                                }
                                else if (laserconverted < laserconverted_end)
                                {
                                    c2s_data.Add(string.Format("FLK\t{0}\t{1}\t{2}\t{3}", beat - 1, offset * 2, laserconverted, flk_size));
                                }
                            }
                            c2s_data.Add(string.Format("SLC\t{0}\t{1}\t{2}\t4\t{3}\t{4}\t4", beat - 1, offset * 2, laserconverted, long_length, laserconverted_end));
                            //c2s_data.Add(string.Format("AHD\t{0}\t{1}\t{2}\t4\t{3}", beat - 1, offset * 2, laserconverted, long_length));
                        }


                    }

                    if (sp[2] == "2")
                    {
                        string[] sp_2 = laserL_data[i - 1].Split('\t');
                        string[] pos_2 = sp_2[0].Split(',');
                        int laserconverted_end = laser_conv(int.Parse(sp_2[1]));
                        int beat_1 = int.Parse(pos_2[0]);
                        int bar_1 = int.Parse(pos_2[1]);
                        int tick_1 = int.Parse(pos_2[2]);
                        long_length = (((beat - beat_1) * 192) + ((bar - bar_1) * 48) + (tick - tick_1)) * 2;
                        int offset_1 = ((bar_1 - 1) * 48) + tick_1;
                        if (long_length == 0)
                        {
                            int flk_size = 0;
                            flk_size = laserconverted + laserconverted_end + 4;
                            if(laserconverted > laserconverted_end)
                            {
                                c2s_data.Add(string.Format("FLK\t{0}\t{1}\t{2}\t{3}", beat_1 - 1, offset_1 * 2, laserconverted_end, flk_size));
                            }
                            else if (laserconverted < laserconverted_end)
                            {
                                c2s_data.Add(string.Format("FLK\t{0}\t{1}\t{2}\t{3}", beat_1 - 1, offset_1 * 2, laserconverted, flk_size));
                            }
                        }
                        c2s_data.Add(string.Format("SLD\t{0}\t{1}\t{2}\t4\t{3}\t{4}\t4", beat_1 - 1, offset_1 * 2, laserconverted_end, long_length, laserconverted));
                        //c2s_data.Add(string.Format("AHD\t{0}\t{1}\t{2}\t4\t{3}", beat - 1, offset * 2, laserconverted_end, long_length));
                        //i++;
                        laser_L = false;
                    }

                }
                for (int i = 0; i < laserR_data.Count; i++)
                {
                    string[] sp = laserR_data[i].Split('\t');
                    string[] pos = sp[0].Split(',');

                    int laserconverted = laser_conv(int.Parse(sp[1]));
                    int beat = int.Parse(pos[0]);
                    int bar = int.Parse(pos[1]);
                    int tick = int.Parse(pos[2]);
                    int offset = ((bar - 1) * 48) + tick;
                    int long_length = 0;
                    if (i < laserR_data.Count - 1)
                    {
                        laser_L = true;
                        if (sp[2] != "2" && laserR_data[i + 1].Split('\t')[2] != "2")
                        {
                            string[] sp_2 = laserR_data[i + 1].Split('\t');
                            string[] pos_2 = sp_2[0].Split(',');
                            int laserconverted_end = laser_conv(int.Parse(sp_2[1]));
                            int beat_1 = int.Parse(pos_2[0]);
                            int bar_1 = int.Parse(pos_2[1]);
                            int tick_1 = int.Parse(pos_2[2]);
                            long_length = (((beat_1 - beat) * 192) + ((bar_1 - bar) * 48) + (tick_1 - tick)) * 2;
                            if (long_length == 0)
                            {
                                int flk_size = 0;
                                flk_size = laserconverted + laserconverted_end + 4;
                                if (laserconverted > laserconverted_end)
                                {
                                    c2s_data.Add(string.Format("FLK\t{0}\t{1}\t{2}\t{3}", beat - 1, offset * 2, laserconverted_end, flk_size));
                                }
                                else if (laserconverted < laserconverted_end)
                                {
                                    c2s_data.Add(string.Format("FLK\t{0}\t{1}\t{2}\t{3}", beat - 1, offset * 2, laserconverted, flk_size));
                                }                                    
                            }
                            c2s_data.Add(string.Format("SLC\t{0}\t{1}\t{2}\t4\t{3}\t{4}\t4", beat - 1, offset * 2, laserconverted, long_length, laserconverted_end));
                            //c2s_data.Add(string.Format("AHD\t{0}\t{1}\t{2}\t4\t{3}", beat - 1, offset * 2, laserconverted, long_length));
                            // i++;
                            laser_L = false;
                        }

                    }
                    if (sp[2] == "2")
                    {
                        string[] sp_2 = laserR_data[i - 1].Split('\t');
                        string[] pos_2 = sp_2[0].Split(',');
                        int laserconverted_end = laser_conv(int.Parse(sp_2[1]));
                        int beat_1 = int.Parse(pos_2[0]);
                        int bar_1 = int.Parse(pos_2[1]);
                        int tick_1 = int.Parse(pos_2[2]);
                        int offset_1 = ((bar_1 - 1) * 48) + tick_1;
                        long_length = (((beat - beat_1) * 192) + ((bar - bar_1) * 48) + (tick - tick_1)) * 2;
                        if (long_length == 0)
                        {
                            int flk_size = 0;
                            flk_size = laserconverted + laserconverted_end + 4;
                            if (laserconverted > laserconverted_end)
                            {
                                c2s_data.Add(string.Format("FLK\t{0}\t{1}\t{2}\t{3}", beat_1 - 1, offset_1 * 2, laserconverted_end, flk_size));
                            }
                            else if (laserconverted < laserconverted_end)
                            {
                                c2s_data.Add(string.Format("FLK\t{0}\t{1}\t{2}\t{3}", beat_1 - 1, offset_1 * 2, laserconverted, flk_size));
                            }
                        }
                        c2s_data.Add(string.Format("SLD\t{0}\t{1}\t{2}\t4\t{3}\t{4}\t4", beat_1 - 1, offset_1 * 2, laserconverted_end, long_length, laserconverted));
                        //c2s_data.Add(string.Format("AHD\t{0}\t{1}\t{2}\t4\t{3}", beat - 1, offset * 2, laserconverted_end, long_length));
                    }
                }
                int laser_conv(int sd)
                {
                    return (int)Math.Round(sd * 0.0944);
                }
                File.WriteAllLines("result.c2s", c2s_data);
                Console.WriteLine("file result.c2s has been saved");
            }
            else
            {
                Console.WriteLine("usage: vox2c2s.exe <input>");
            }
        }
    }
}
