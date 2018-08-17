﻿using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Ivony.Web
{
  public static class SimpleRouteExtensions
  {
    private const string DefaultSimpleRouteTableKey = "SimpleRouteTable";


    /// <summary>
    /// 将对象转换为属性映射表
    /// </summary>
    /// <param name="obj">要转换的对象</param>
    /// <returns></returns>
    public static IDictionary<string, string> ToPropertyMapping( object obj )
    {

      var values = new Dictionary<string, string>();
      if ( obj == null )
        return values;


      if ( obj is IDictionary dictionary )
      {
        if ( dictionary is IDictionary<string, string> result )
          return result;

        foreach ( var key in dictionary.Keys )
          values.Add( key.ToString(), dictionary[key]?.ToString() );
      }
      else
      {

        if ( obj != null )
        {
          foreach ( PropertyDescriptor property in TypeDescriptor.GetProperties( obj ) )
            values.Add( property.Name, property.GetValue( obj )?.ToString() );
        }
      }

      return values;

    }



    /// <summary>
    /// 添加一个路由规则
    /// </summary>
    /// <param name="name">规则名称</param>
    /// <param name="urlPattern">URL 模式</param>
    /// <param name="routeValues">静态/默认路由值</param>
    /// <param name="queryKeys">可用于 QueryString 的参数，若为null则表示无限制</param>
    public static IRouteBuilder MapSimple( this IRouteBuilder builder, string urlPattern, object routeValues )
    {
      return MapSimple( builder, urlPattern, ToPropertyMapping( routeValues ) );
    }


    /// <summary>
    /// 添加一个路由规则
    /// </summary>
    /// <param name="name">规则名称</param>
    /// <param name="urlPattern">URL 模式</param>
    /// <param name="routeValues">静态/默认路由值</param>
    /// <param name="queryKeys">可用于 QueryString 的参数，若为null则表示无限制</param>
    public static IRouteBuilder MapSimple( this IRouteBuilder builder, string urlPattern, IDictionary<string, string> routeValues )
    {
      return MapSimple( builder, "{" + urlPattern + "}", urlPattern, routeValues, null );
    }


    /// <summary>
    /// 添加一个路由规则
    /// </summary>
    /// <param name="name">规则名称</param>
    /// <param name="urlPattern">URL 模式</param>
    /// <param name="routeValues">静态/默认路由值</param>
    /// <param name="queryKeys">可用于 QueryString 的参数，若为null则表示无限制</param>
    public static IRouteBuilder MapSimple( this IRouteBuilder builder, string urlPattern, object routeValues, IReadOnlyCollection<string> queryKeys )
    {
      return MapSimple( builder, urlPattern, ToPropertyMapping( routeValues ), queryKeys );
    }
    /// <summary>
    /// 添加一个路由规则
    /// </summary>
    /// <param name="name">规则名称</param>
    /// <param name="urlPattern">URL 模式</param>
    /// <param name="routeValues">静态/默认路由值</param>
    /// <param name="queryKeys">可用于 QueryString 的参数，若为null则表示无限制</param>
    public static IRouteBuilder MapSimple( this IRouteBuilder builder, string urlPattern, IDictionary<string, string> routeValues, IReadOnlyCollection<string> queryKeys )
    {
      return MapSimple( builder, "{" + urlPattern + "}", urlPattern, routeValues, queryKeys );
    }


    /// <summary>
    /// 添加一个路由规则
    /// </summary>
    /// <param name="name">规则名称</param>
    /// <param name="urlPattern">URL 模式</param>
    public static IRouteBuilder MapSimple( this IRouteBuilder builder, string name, string urlPattern )
    {
      return MapSimple( builder, name, urlPattern, new Dictionary<string, string>(), new string[0] );
    }


    /// <summary>
    /// 添加一个路由规则
    /// </summary>
    /// <param name="name">规则名称</param>
    /// <param name="urlPattern">URL 模式</param>
    /// <param name="routeValues">静态/默认路由值</param>
    public static IRouteBuilder MapSimple( this IRouteBuilder builder, string name, string urlPattern, object routeValues )
    {
      return MapSimple( builder, name, urlPattern, ToPropertyMapping( routeValues ) );
    }
    /// <summary>
    /// 添加一个路由规则
    /// </summary>
    /// <param name="name">规则名称</param>
    /// <param name="urlPattern">URL 模式</param>
    /// <param name="routeValues">静态/默认路由值</param>
    public static IRouteBuilder MapSimple( this IRouteBuilder builder, string name, string urlPattern, IDictionary<string, string> routeValues )
    {
      return MapSimple( builder, name, null, false, urlPattern, routeValues, null );
    }



    /// <summary>
    /// 添加一个路由规则
    /// </summary>
    /// <param name="name">规则名称</param>
    /// <param name="urlPattern">URL 模式</param>
    /// <param name="routeValues">静态/默认路由值</param>
    /// <param name="queryKeys">可用于 QueryString 的参数，若为null则表示无限制</param>
    public static IRouteBuilder MapSimple( this IRouteBuilder builder, string name, string urlPattern, object routeValues, IReadOnlyCollection<string> queryKeys )
    {
      return MapSimple( builder, name, urlPattern, ToPropertyMapping( routeValues ), queryKeys );
    }
    /// <summary>
    /// 添加一个路由规则
    /// </summary>
    /// <param name="name">规则名称</param>
    /// <param name="urlPattern">URL 模式</param>
    /// <param name="routeValues">静态/默认路由值</param>
    /// <param name="queryKeys">可用于 QueryString 的参数，若为null则表示无限制</param>
    public static IRouteBuilder MapSimple( this IRouteBuilder builder, string name, string urlPattern, IDictionary<string, string> routeValues, IReadOnlyCollection<string> queryKeys )
    {
      return MapSimple( builder, name, null, false, urlPattern, routeValues, queryKeys );
    }



    /// <summary>
    /// 添加一个路由规则
    /// </summary>
    /// <param name="name">规则名称</param>
    /// <param name="verb">HTTP 动词</param>
    /// <param name="oneway">是否创建为单向路由</param>
    /// <param name="urlPattern">URL 模式</param>
    /// <param name="routeValues">静态/默认路由值</param>
    /// <param name="queryKeys">可用于 QueryString 的参数，若为null则表示无限制</param>
    public static IRouteBuilder MapSimple( this IRouteBuilder builder, string name, string verb, bool oneway, string urlPattern, object routeValues, IReadOnlyCollection<string> queryKeys )
    {
      return MapSimple( builder, name, verb, oneway, urlPattern, ToPropertyMapping( routeValues ), queryKeys );
    }
    /// <summary>
    /// 添加一个路由规则
    /// </summary>
    /// <param name="name">规则名称</param>
    /// <param name="verb">HTTP 动词</param>
    /// <param name="oneway">是否创建为单向路由</param>
    /// <param name="urlPattern">URL 模式</param>
    /// <param name="routeValues">静态/默认路由值</param>
    /// <param name="queryKeys">可用于 QueryString 的参数，若为null则表示无限制</param>
    public static IRouteBuilder MapSimple( this IRouteBuilder builder, string name, string verb, bool oneway, string urlPattern, IDictionary<string, string> routeValues, IReadOnlyCollection<string> queryKeys )
    {
      var routeTable = SimpleRouteTable( builder );
      routeTable.AddRule( name, verb, oneway, urlPattern, routeValues, queryKeys );
      return builder;
    }


    /// <summary>
    /// 添加一个路由规则
    /// </summary>
    /// <param name="name">规则名称</param>
    /// <param name="urlPattern">URL 模式</param>
    /// <param name="routeValues">静态/默认路由值</param>
    public static SimpleRouteRule AddRule( this SimpleRouteTable routeTable, string name, string urlPattern, IDictionary<string, string> routeValues )
    {
      return AddRule( routeTable, name, urlPattern, routeValues, null );
    }

    /// <summary>
    /// 添加一个路由规则
    /// </summary>
    /// <param name="name">规则名称</param>
    /// <param name="urlPattern">URL 模式</param>
    /// <param name="routeValues">静态/默认路由值</param>
    /// <param name="queryKeys">可用于 QueryString 的参数，若为null则表示无限制</param>
    public static SimpleRouteRule AddRule( this SimpleRouteTable routeTable, string name, string urlPattern, IDictionary<string, string> routeValues, IReadOnlyCollection<string> queryKeys )
    {
      return routeTable.AddRule( name, null, false, urlPattern, routeValues, queryKeys );
    }



    /// <summary>
    /// 获取默认的 SimpleRouteTable 对象
    /// </summary>
    /// <param name="builder">IRouteBuilder 对象</param>
    /// <returns></returns>
    public static SimpleRouteTable SimpleRouteTable( this IRouteBuilder builder )
    {
      var routeTable = builder.Routes.OfType<SimpleRouteTable>().FirstOrDefault();
      if ( routeTable == null )
      {
        var loggerProvider = (ILoggerProvider) builder.ApplicationBuilder.ApplicationServices.GetService( typeof( ILoggerProvider ) );
        builder.Routes.Add( routeTable = new SimpleRouteTable( "Default", loggerProvider.CreateLogger( "Simple Route Table" ), builder.DefaultHandler ) );
      }

      return routeTable;
    }
  }
}