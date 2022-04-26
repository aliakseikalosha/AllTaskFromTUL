//
// Created by Aliaksei Kalosha on 21.03.2022.
//

#pragma once

#include "glm/gtc/type_ptr.hpp"
#include "./Avatars/Camera.h"
#include "./Avatars/Player.h"
#include "../GL/input.h"
#include "Avatars/SceneObject.h"
#include "../DataLoader/texture.h"

#define WORLD_CELL_SIZE (5.0f)

class Game {
protected:
    glm::vec3 offset = glm::vec3(0, 0, 0);
public:
    Game() { Init(); }

    ~Game() {
        delete player;
        delete camera;
    }

    Player *player;
    Camera *camera;
    std::vector<SceneObject> objects;
    glm::mat4 viewMat;

    void Init() {
        player = new Player(glm::vec3(0, 0, 0), glm::vec2(0, 0), 5);
        camera = new Camera(glm::vec3(0, 1, -5), glm::vec2(0, 0));
        loadLevel("../resources/map/lvl01.txt");
    }

    void loadLevel(const char *levelPath) {
        std::ifstream t(levelPath);
        std::string str((std::istreambuf_iterator<char>(t)),
                        std::istreambuf_iterator<char>());
        GLuint textureId = textureInit("../resources/texture/0x72_DungeonTilesetII_v1.4.png", false);
        SceneObject *sceneObject;
        Mesh *m;
        for (int x = 0; x < 10; ++x) {
            for (int z = 0; z < 10; ++z) {
                sceneObject = new SceneObject(glm::vec3(x - 5.0, -1, z - 5.0), glm::vec2(0, 0));
                sceneObject->renderer = new MeshRenderer();
                m = new Mesh();

                m->loadFloor(glm::vec2((2 * 16.0) / 512.0, (5 * 16.0) / 512.0), 16.0 / 512.0);
                sceneObject->renderer->init(*m, "../resources/shader/texture.vert", "../resources/shader/texture.frag", textureId);
                objects.push_back(*sceneObject);
            }
        }
    }

    void placeWall(float x, float y,float z){
        SceneObject *wall = new SceneObject(glm::vec3(x,y,z), glm::vec2(0.0,0.0));
        Mesh *m = new Mesh();

    }

    void Update(const float &dt) {
        player->ProcessInput(inputState.moveDelta, inputState.lookDelta, dt);
        camera->UpdateFollow(*player, offset);
        viewMat = glm::lookAt(camera->getPos(), camera->forvard() + camera->getPos(), glm::vec3(0.0f, 1.0f, 0.0f));
    }

    void Draw(glm::mat4 &projectionMatrix, const float &dt) {
        for (auto &obj: objects) // access by reference to avoid copying
        {
            if (obj.renderer != NULL) {
                obj.renderer->render(projectionMatrix, viewMat, dt, obj.getPos());
            }
        }
    }
};
