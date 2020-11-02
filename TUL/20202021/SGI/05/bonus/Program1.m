clc;clear all;close all;
numbers = imread('numbers_small.jpg');
for x=1:10
    for y = 1:10
        xIndex = (11:111)+ (x-1)*120;
        yIndex = (11:111)+ (y-1)*120;
        n = numbers(xIndex, yIndex);
        n = imresize(n,[32,32]);
        n(:,[1,2,end-1,end]) = 255;
        n([1,2,end-1,end],:) = 255;
        imwrite(n,sprintf("c%d_p0000_s%02d.jpg",x-1,y));
    end
end