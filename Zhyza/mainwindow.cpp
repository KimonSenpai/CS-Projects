#include "mainwindow.h"
#include "ui_mainwindow.h"
#include <QPainter>
MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow)
{
    ui->setupUi(this);
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::update(){
    QPainter* paint = new QPainter();
   // paint->pen().setColor(QColor(0,0,0));

    paint->begin(this);
    paint->setPen(QColor(0,0,0));
    paint->drawLine(20,20,100,100);
    paint->end();
    delete paint;

}

void MainWindow::paintEvent(QPaintEvent *event){
    QPainter* paint = new QPainter();
   // paint->pen().setColor(QColor(0,0,0));

    paint->begin(this);
    paint->setPen(QColor(0,0,0));
    paint->drawLine(20,20,100,100);
    paint->end();
    delete paint;
}

void MainWindow::Start_Click(){
    QPainter* paint = new QPainter();
   // paint->pen().setColor(QColor(0,0,0));

    paint->begin(ui->centralWidget);
    paint->setPen(QColor(0,0,0));
    paint->drawLine(20,20,100,100);
    paint->end();
    delete paint;
    repaint();
}
