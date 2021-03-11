clear all
clc
%pozor na nedefinované řádky, nutno použít příkaz s předponou nan

vstup=importdata('P0330.xlsx')
%výsledky ve formě struktury, vstupy jsou uloženy  v listu1
x=vstup.data.List1;

for i=1:5
    str_hod=nanmean(x(:,i))
    var_hod=nanvar(x(:,i))
    std_hod=nanstd(x(:,i))
end