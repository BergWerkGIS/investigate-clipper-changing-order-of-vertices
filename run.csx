#r "Mapbox.VectorTile.Geometry.dll"
#r "Mapbox.VectorTile.VectorTileReader.dll"

using System.IO;
using Mapbox.VectorTile;
using Mapbox.VectorTile.Geometry;


private void dumpGeom(List<List<Point2d<long>>> geom){

	foreach(var part in geom){
		foreach(var coord in part){
			Console.WriteLine($"              {coord.X}/{coord.Y}");
		}
	}
}


private void dumpFeatures(string file) {

	Console.WriteLine($"---- {file} ----");
	byte[] data = File.ReadAllBytes(file);
	VectorTile vt = new VectorTile(data);
	foreach (var lyrName in vt.LayerNames()) {
		VectorTileLayer lyr = vt.GetLayer(lyrName);
		Console.WriteLine($"layer name  : {lyr.Name}");
		Console.WriteLine($"extent      : {lyr.Extent}");
		int featCnt = lyr.FeatureCount();
		for (int i = 0; i < featCnt;i++){
			VectorTileFeature<long> feat = lyr.GetFeature<long>(i);
			VectorTileFeature<long> featClip = lyr.GetFeature<long>(i,0);
			Console.WriteLine($"geom type   : {feat.GeometryType}");
			Console.WriteLine("not clipped :");
			dumpGeom(feat.Geometry);
			Console.WriteLine("    clipped :");
			dumpGeom(featClip.Geometry);
		}
	}
}

string[] files = new string[]{
	"Feature-single-linestring.mvt",
	"Feature-single-polygon.mvt"
};

foreach(var file in files){
	dumpFeatures(file);
}