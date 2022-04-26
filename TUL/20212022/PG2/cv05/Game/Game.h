//
// Created by Aliaksei Kalosha on 21.03.2022.
//

#pragma once
#include "glm/gtc/type_ptr.hpp"
#include "./Avatars/Camera.h"
#include "./Avatars/Player.h"
#include "../GL/input.h"
#include "Avatars/SceneObject.h"

class Game {
protected:
    glm::vec3 offset = glm::vec3(0, 0, 0);
public:
    Game() { Init(); }
    ~Game(){
        delete player;
        delete camera;
    }
    Player *player;
    Camera *camera;
    std::vector<SceneObject> objects;
    glm::mat4 viewMat;

    void Init() {
        player = new Player(glm::vec3(0, 0, 0), glm::vec2(0, 0), 5);
        camera = new Camera(glm::vec3(0, 0, -5), glm::vec2(0, 0));
        SceneObject *sceneObject;
        Mesh m;
        for (int x = 0; x < 10; ++x) {
            for (int z = 0; z < 10; ++z) {
                sceneObject = new SceneObject(glm::vec3(x-5.0,z-5.0,-2), glm::vec2(0,0));
                sceneObject->renderer = new MeshRenderer();
                sceneObject->renderer->init(m, sceneObject->getPos());
                objects.push_back(*sceneObject);
            }
        }
    }

    void Update(const float &dt) {
        player->ProcessInput(inputState.moveDelta, inputState.lookDelta, dt);
        camera->UpdateFollow(*player, offset);
        viewMat = glm::lookAt(camera->getPos(), camera->forvard() + camera->getPos(), glm::vec3(0.0f, 1.0f, 0.0f));
    }

    void Draw(glm::mat4 &projectionMatrix, const float &dt){
        for (auto &obj : objects) // access by reference to avoid copying
        {
            if(obj.renderer != NULL)
            {
                obj.renderer->render(projectionMatrix, viewMat, dt, obj.getPos());
            }
        }
    }
};
