#pragma once
#ifndef OBJloader_H
#define OBJloader_H

#include <vector>
#include <string>
#include <glm/fwd.hpp>

#include "mesh.h"

mesh loadOBJ(std::string model_path);
mesh loadOBJtex(std::string obj_path, std::string texture_path);
bool loadTexture(mesh& out_mesh, std::string texture_path);

#endif
