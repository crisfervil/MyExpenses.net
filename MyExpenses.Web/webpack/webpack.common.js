﻿var webpack = require('webpack');
var HtmlWebpackPlugin = require('html-webpack-plugin');
var ExtractTextPlugin = require('extract-text-webpack-plugin');
var helpers = require('./helpers');

module.exports = {
    entry: {
        'polyfills': './Public/polyfills.ts',
        'vendor': './Public/vendor.ts',
        'app': './Public/main.ts'
    },

    resolve: {
        extensions: ['', '.ts', '.js']
    },

    module: {
        loaders: [
          {
              test: /\.ts$/,
              loaders: ['awesome-typescript-loader', 'angular2-template-loader']
          },
          {
              test: /\.html$/,
              loader: 'html'
          },
          {
              test: /\.(png|jpe?g|gif|svg|woff|woff2|ttf|eot|ico)$/,
              loader: 'file?name=assets/[name].[hash].[ext]'
          },
          {
              test: /\.css$/,
              exclude: helpers.root('Public', 'app'),
              loader: ExtractTextPlugin.extract('style', 'css?sourceMap')
          },
          {
              test: /\.css$/,
              include: helpers.root('Public', 'app'),
              loader: 'raw'
          }
        ]
    },

    plugins: [
      new webpack.optimize.CommonsChunkPlugin({
          name: ['app', 'vendor', 'polyfills']
      }),

      new HtmlWebpackPlugin({
          template: 'Public/index.html'
      })
    ]
};
