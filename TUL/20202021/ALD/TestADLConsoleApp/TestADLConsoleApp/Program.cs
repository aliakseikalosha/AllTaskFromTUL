using c = System.Console;
string GetLine(int i, int w, ref int s,string[]d){var res=d[s++];int j=0,l=d.Length;int a()=>res.Replace("{","").Replace("}","").Length;while(s<l&&a()<w){if(a()+d[s].Length>=w)break;res+="{"+j+++"}"+d[s++];}var p=new string[j+1];if(s>=l){for(int k=0;k<j+1;)p[k++]+=" ";return"".PadLeft(i)+string.Format(res,p);}var o="";do{p[j++%p.Length]+=" ";o=string.Format(res,p);}
while(o.Length<w);return"".PadLeft(i)+o;}
foreach(var a in args)
{var s=a.Split("\n");int i=int.Parse(s[0].Split()[0]),w=int.Parse(s[0].Split()[1]),z=0;for(int j=1;j<s.Length;j++){
var d=s[j].Split();
int k=0;
void h(){z++;if(z%2<1){w+=2;i-=i>0?1:0;}}
void y()=>c.WriteLine(GetLine(i,w,ref k,d));h();
for(;k<d.Length;)y();
c.WriteLine();h();}}