import java.awt.*;
import java.awt.event.*;
import java.awt.geom.AffineTransform;
import java.awt.geom.GeneralPath;

import javax.swing.*;
import javax.imageio.ImageIO;
import java.awt.image.BufferedImage;
import java.io.IOException;

public class Transforms2D extends JPanel {

	private class Display extends JPanel {
		protected void paintComponent(Graphics g) {
			super.paintComponent(g);
			Graphics2D g2 = (Graphics2D)g;
			g2.setRenderingHint(RenderingHints.KEY_ANTIALIASING, RenderingHints.VALUE_ANTIALIAS_ON);
			g2.translate(300,300);  // Moves (0,0) to the center of the display.
			int index = transformSelect.getSelectedIndex();
			
			MainDraw(g2,150d);
		
		
	}
	}
	private void MainDraw(Graphics2D g2,double r)
	{
		
		int[] pointX = new int[13];
		int[] pointY = new int[13];
		
		for(int i = 0; i<13 ; i++)
		{
			pointX[i] = (int) (0 + (r * Math.sin( i * (Math.PI*2/13) )));
			pointY[i] = (int) (0 + (r * Math.cos( i * (Math.PI*2/13) )));
		}
		AffineTransform transform = new AffineTransform();
		int index = transformSelect.getSelectedIndex();
		if(index == 1)
		{
			transform.scale(0.5, 0.5);
		}
		if(index == 2)
		{
			transform.rotate(Math.PI/4);
		}
		if(index == 3 || index == 7)
		{
			transform.rotate(Math.PI);
			transform.scale(0.5,1);
		}
		if(index == 4)
		{
			transform.shear(0.5,0);
		}
		if(index == 5)
		{
			transform.translate(0, -g2.getClipBounds().height/2 + r * 0.5);
		
			transform.scale(1,0.5);
			
		}
		if(index == 6)
		{
			transform.rotate(Math.PI/2);
			transform.shear(0.5,0);
		
		}
		if(index == 8)
		{
			transform.rotate(Math.PI/4);
			transform.translate(0,r);
			transform.scale(1,0.5);
			
		}
		if(index == 9)
		{
			transform.translate(600/2 - 300 * Math.tan(0.5),0);
			transform.rotate(Math.PI);
			
			transform.shear(0.5,0);
			
		}
		
		
		Shape polygon = new Polygon(pointX,pointY,13);
		Shape arrow = Arrow(g2);
		polygon = transform.createTransformedShape(polygon);
		arrow = transform.createTransformedShape(arrow);
		
		g2.setColor(Color.BLACK);
		g2.setPaint(Color.BLACK);
		g2.fill(polygon);
		
		
		g2.setColor(Color.RED);
	 	g2.setStroke(new BasicStroke(6));
	 	g2.draw(arrow);
		
       
		
		
	}
	private Shape Arrow(Graphics2D g2)
	{
			
		 
	        int startX = 0;
	        int startY = 140;
	        int endX = 0;
	        int endY = -140;
	        int arrowLength = 10;
	        int arrowWidth = 10;
	        double angle = Math.atan2(endY - startY, endX - startX);
	        int arrowEndX = (int) (endX - arrowLength * Math.cos(angle));
	        int arrowEndY = (int) (endY - arrowLength * Math.sin(angle));
	        
	       
	        GeneralPath arrow = new GeneralPath();
	        arrow.moveTo(startX,startY);
	        arrow.lineTo(endX, endY);
	        
	        Polygon arrowHead = new Polygon();
				        
	        arrowHead.addPoint((int)(arrowEndX + (arrowWidth  * Math.sin(Math.PI - angle))),(int) (arrowEndY + (arrowWidth  * Math.cos(Math.PI - angle))));
	        arrowHead.addPoint((int)(arrowEndX - (arrowWidth  * Math.sin(Math.PI - angle))),(int) (arrowEndY - (arrowWidth  * Math.cos(Math.PI - angle))));
	        arrowHead.addPoint((int)(arrowEndX + (arrowWidth  * Math.sin(Math.PI/2 - angle))),(int) (arrowEndY + (arrowWidth  * Math.cos(Math.PI/2 - angle))));
	        
	        arrow.append(arrowHead, false);
	        
	        return arrow;
	        
	}
	
	private Display display;
	private BufferedImage pic;
	private JComboBox<String> transformSelect;

	public Transforms2D() throws IOException {
		pic = ImageIO.read(getClass().getClassLoader().getResource("shuttle.jpg"));
		display = new Display();
		display.setBackground(Color.YELLOW);
		display.setPreferredSize(new Dimension(600,600));
		transformSelect = new JComboBox<String>();
		transformSelect.addItem("None");
		for (int i = 1; i < 10; i++) {
			transformSelect.addItem("No. " + i);
		}
		transformSelect.addActionListener( new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				display.repaint();
			}
		});
		setLayout(new BorderLayout(3,3));
		setBackground(Color.GRAY);
		setBorder(BorderFactory.createLineBorder(Color.GRAY,10));
		JPanel top = new JPanel();
		top.setLayout(new FlowLayout(FlowLayout.CENTER));
		top.setBorder(BorderFactory.createEmptyBorder(4, 4, 4, 4));
		top.add(new JLabel("Transform: "));
		top.add(transformSelect);
		add(display,BorderLayout.CENTER);
		add(top,BorderLayout.NORTH);
	}


	public static void main(String[] args) throws IOException {
		JFrame window = new JFrame("2D Transforms");
		window.setContentPane(new Transforms2D());
		window.pack();
		window.setResizable(false);
		window.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		Dimension screen = Toolkit.getDefaultToolkit().getScreenSize();
		window.setLocation( (screen.width - window.getWidth())/2, (screen.height - window.getHeight())/2 );
		window.setVisible(true);
	}

}