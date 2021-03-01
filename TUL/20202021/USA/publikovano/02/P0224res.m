%selže aktivní hasicí systém
AHSneg=0.05;
%selže signalizace na centrální pult
SCPneg=0.08;
%selžou oba systémy P(AHSnegace prunik SCP negace)
AHSneg_prunik_SCPneg=0.03;

%ad a)
%pravdìpodobnost selže hasicí systém a selže i SCP
AHSneg_prunik_SCPneg=0.03;
%pravdìpodobnost selže hasicí systém a neselže SCP je doplnìk
vysledek_a=AHSneg-AHSneg_prunik_SCPneg

%ad b)
%ètyøi rùzné jevy, zapùsobí oba, zapùjobí AHS, zapùsobí SCP, nezapùsobí oba
vysledek_b=1-AHSneg-SCPneg+AHSneg_prunik_SCPneg

