using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace update_csd
{
    class Program
    {
        static String url = "http://csd.hoge.com/servers/update";
        static String pc_name = "";

        static void Main(string[] args)
        {
            double cpu_par = cpu_parse(get_cpu_usage(), get_cpu_number());
            send_data(pc_name, cpu_par.ToString(), get_mem_used(get_mem_free()).ToString(), get_mem_free().ToString(), get_mem_swap().ToString(), get_operating_time().ToString(), get_process_number().ToString(), "0", "null", "null", "0");
        }

        static void send_data(String name, String cpu, String mem_used, String mem_free, String mem_swap, String time, String process, String zombie, String high_cpu, String high_mem, String temp)
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            System.Collections.Specialized.NameValueCollection post = new System.Collections.Specialized.NameValueCollection();
            post.Add("NAME", name);
            post.Add("CPU", cpu);
            post.Add("MEM_USED", mem_used);
            post.Add("MEM_FREE", mem_free);
            post.Add("MEM_SWAP", mem_swap);
            post.Add("Operating_time", time);
            post.Add("Process_number", process);
            post.Add("Zombie_process", zombie);
            post.Add("High_CPU_Process", high_cpu);
            post.Add("High_MEM_Process", high_mem);
            post.Add("TEMP", temp);

            byte[] resPost = wc.UploadValues(url, post);
            Console.WriteLine(System.Text.Encoding.UTF8.GetString(resPost));

        }

        static double get_cpu_usage()
        {
            System.Diagnostics.PerformanceCounter pc = new System.Diagnostics.PerformanceCounter("Processor", "% Processor Time", "_Total", true);
            double cpu = pc.NextValue();
            Thread.Sleep(1000);
            cpu = pc.NextValue();
            return cpu;
        }
        
        static double get_cpu_number()
        {
            double cpu = Environment.ProcessorCount;
            return cpu;
        }

        static double cpu_parse(double usage, double num)
        {
            return (usage * 0.01) * num;
        }

        static Microsoft.VisualBasic.Devices.ComputerInfo mem_info = new Microsoft.VisualBasic.Devices.ComputerInfo();

        static double get_mem_free()
        {
            double free = mem_info.AvailablePhysicalMemory;
            return free * 0.001;
        }
        
        static double get_mem_used(double mem_free)
        {
            double total = mem_info.TotalPhysicalMemory;
            return (total - mem_free) * 0.001;
        }

        static double get_mem_swap()
        {
            double total_swap = mem_info.TotalVirtualMemory;
            double free_swap = mem_info.AvailableVirtualMemory;
            return (total_swap - free_swap) * 0.001;
        }

        static double get_operating_time()
        {
            double tickCount = System.Environment.TickCount & int.MaxValue;
            return (tickCount * 0.001) / 60;
        }

        static int get_process_number()
        {
            System.Management.ManagementClass mc = new System.Management.ManagementClass("Win32_Process");
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            int i = 0;
            foreach (System.Management.ManagementObject mo in moc)
            {
                i++;
            }
            return i;
        }
    }
}
