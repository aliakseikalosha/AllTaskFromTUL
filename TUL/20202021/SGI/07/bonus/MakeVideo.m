clear all;clc;close all
video = VideoWriter('prediction.avi'); %create the video object
open(video); %open the file for writing
for d=219:278 %where N is the number of images
  I = imread(sprintf("preditiction_for_%d.png",d)); %read the next image
  writeVideo(video,I); %write the image to file
 end
close(video); %close the file