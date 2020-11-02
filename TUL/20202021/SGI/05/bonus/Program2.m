clc;clear all;close all;
handleToAxes = figure();
for x=1:10
    for y = 1:10
        subplot(2,5,y);
        n = imread(sprintf("c%d_p0000_s%02d.jpg",x-1,y));
        image(n);
        colormap(handleToAxes, gray(256));
        title(sprintf("%d:%02d",x-1,y));
    end
    pause
end