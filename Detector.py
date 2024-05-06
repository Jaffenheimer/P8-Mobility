from detectron2.engine import DefaultPredictor
from detectron2.config import get_cfg
from detectron2.data import MetadataCatalog
from detectron2.utils.visualizer import ColorMode, Visualizer
from detectron2 import model_zoo
from datetime import date
from PIL import Image

import cv2
import numpy as np

class Detector:
    def __init__(self):
        self.cfg = get_cfg()

        self.cfg.DETECTIONS_PER_IMAGE = 1000
        self.cfg.merge_from_file(model_zoo.get_config_file("COCO-Detection/faster_rcnn_X_101_32x8d_FPN_3x.yaml"))
        self.cfg.MODEL.WEIGHTS = model_zoo.get_checkpoint_url("COCO-Detection/faster_rcnn_X_101_32x8d_FPN_3x.yaml")
        self.cfg.MODEL.ROI_HEADS.SCORE_THRES_TEST = 0.7
        self.cfg.MODEL.DEVICE = "cpu" # or cpu
        
        self.predictor = DefaultPredictor(self.cfg)

    def imageDetection(self, imagePath):
        image = cv2.imread(imagePath)

        predictions = self.predictor(image)

        _metadata = MetadataCatalog.get(self.cfg.DATASETS.TRAIN[0])

        viz = Visualizer(image[:,:,::-1], metadata = _metadata, instance_mode = ColorMode.IMAGE_BW)
        
        persons = predictions["instances"][predictions["instances"].pred_classes == 0]

        output = viz.draw_instance_predictions(persons.to("cpu"))
        no_of_people = len(persons)

        return no_of_people
        
    
    def CaptureVideo(self, vs):
        stream = cv2.VideoCapture(vs)
        ret, frame = stream.read()
        path = "images/snapshots/img_1.jpg"
        if frame is not None:
                cv2.imwrite(path, frame)
        stream.release()
        return path