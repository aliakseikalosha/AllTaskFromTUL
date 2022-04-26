#pragma once
#ifndef TEXTURE_H
#define TEXTURE_H

#include <opencv2/opencv.hpp>

GLuint textureInit(const char * path, const bool mirror);
GLuint tex_gen(cv::Mat& image);

#endif
