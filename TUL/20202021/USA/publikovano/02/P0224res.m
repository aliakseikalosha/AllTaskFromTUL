%sel�e aktivn� hasic� syst�m
AHSneg=0.05;
%sel�e signalizace na centr�ln� pult
SCPneg=0.08;
%sel�ou oba syst�my P(AHSnegace prunik SCP negace)
AHSneg_prunik_SCPneg=0.03;

%ad a)
%pravd�podobnost sel�e hasic� syst�m a sel�e i SCP
AHSneg_prunik_SCPneg=0.03;
%pravd�podobnost sel�e hasic� syst�m a nesel�e SCP je dopln�k
vysledek_a=AHSneg-AHSneg_prunik_SCPneg

%ad b)
%�ty�i r�zn� jevy, zap�sob� oba, zap�job� AHS, zap�sob� SCP, nezap�sob� oba
vysledek_b=1-AHSneg-SCPneg+AHSneg_prunik_SCPneg

