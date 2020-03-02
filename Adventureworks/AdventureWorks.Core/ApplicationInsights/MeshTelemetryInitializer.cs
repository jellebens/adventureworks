
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;

namespace Adventureworks.Core.ApplicationInsights
{
    /// <summary>
    /// Telemetry initializer for service fabric. Adds service fabric specific context to outgoing telemetry.
    /// </summary>
    public class MeshTelemetryInitializer: ITelemetryInitializer
    {
        
        // If you update this - also update the same constant in src\ApplicationInsights.ServiceFabric.Native\Net45\FabricTelemetryInitializerExtension.cs
        private const string ServiceContextKeyName = "AI.SF.ServiceContext";

        private Dictionary<string, string> contextCollection;

        private Dictionary<string, string> ApplicableServiceContext
        {
            get
            {
                if (this.contextCollection != null && this.contextCollection.Count > 0)
                {
                    return this.contextCollection;
                }

                return CallContext.LogicalGetData(ServiceContextKeyName) as Dictionary<string, string>;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FabricTelemetryInitializer"/> class.
        /// </summary>
        public MeshTelemetryInitializer()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FabricTelemetryInitializer"/> class.
        /// </summary>
        public MeshTelemetryInitializer(Dictionary<string, string> context)
        {
            // Clone the passed in context.
            this.contextCollection = new Dictionary<string, string>(context);
        }

        /// <summary>
        /// Adds service fabric context fields on the given telemetry object.
        /// </summary>
        /// <param name="telemetry">The telemetry item being sent through the AI sdk.</param>
        public void Initialize(ITelemetry telemetry)
        {
            try
            {
                // Populate telemetry context properties from the service context object
                var serviceContext = this.ApplicableServiceContext;
                if (serviceContext != null)
                {
                    foreach (var field in serviceContext)
                    {
                        if (!telemetry.Context.GlobalProperties.ContainsKey(field.Key))
                        {
                            telemetry.Context.GlobalProperties.Add(field.Key, field.Value);
                        }
                    }
                }

                // Populate telemetry context properties from environment variables, but not overwriting properties
                // that have been populated from the service context. The environment variables are basically a fallback mechanism.
                AddPropertyFromEnvironmentVariable(KnownContextFieldNames.ServiceName, KnownEnvironmentVariableName.ServiceName, telemetry);
                AddPropertyFromEnvironmentVariable(KnownContextFieldNames.NodeName, KnownEnvironmentVariableName.NodeName, telemetry);
                AddPropertyFromEnvironmentVariable(KnownContextFieldNames.PartitionId, KnownEnvironmentVariableName.PartitionId, telemetry);
                AddPropertyFromEnvironmentVariable(KnownContextFieldNames.ApplicationName, KnownEnvironmentVariableName.ApplicationName, telemetry);
                AddPropertyFromEnvironmentVariable(KnownContextFieldNames.CodePackageName, KnownEnvironmentVariableName.CodePackageName, telemetry);
                AddPropertyFromEnvironmentVariable(KnownContextFieldNames.DnsName, KnownEnvironmentVariableName.DnsName, telemetry);
                AddPropertyFromEnvironmentVariable(KnownContextFieldNames.ReplicaId, KnownEnvironmentVariableName.ReplicaId, telemetry);
                AddPropertyFromEnvironmentVariable(KnownContextFieldNames.ReplicaName, KnownEnvironmentVariableName.ReplicaName, telemetry);



                telemetry.Context.Cloud.RoleName = $"{telemetry.Context.GlobalProperties[KnownContextFieldNames.DnsName]}";
                telemetry.Context.Cloud.RoleInstance = telemetry.Context.GlobalProperties[KnownContextFieldNames.NodeName];

                
            }
            catch
            {
                // Something went wrong trying to set these extra properties. We shouldn't fail though.
            }
        }

        private void AddPropertyFromEnvironmentVariable(object environmnet, object environment, ITelemetry telemetry)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds the property to the telemetry context, if it doesn't already exist, using the environment variable value. It's a no-op
        /// if the property with the <paramref name="contextFieldName"/> already exist in the telemetry context.
        /// </summary>
        /// <param name="contextFieldName">The name of context field property, as used by Service Fabric. This will be same name used in the telemetry context property dictionary</param>
        /// <param name="environmentVariableName">The name of the environment variable having the equivalent value</param>
        /// <param name="telemetry">The telemetry object whose property dictionary will be updated</param>
        private void AddPropertyFromEnvironmentVariable(string contextFieldName, string environmentVariableName, ITelemetry telemetry)
        {
            if (!telemetry.Context.GlobalProperties.ContainsKey(contextFieldName))
            {
                string value = Environment.GetEnvironmentVariable(environmentVariableName);
                if (!string.IsNullOrEmpty(value))
                {
                    telemetry.Context.GlobalProperties.Add(contextFieldName, value);
                }
            }
        }

        // If you update this - also update the same constant in src\ApplicationInsights.ServiceFabric.Native\Net45\FabricTelemetryInitializerExtension.cs
        private class KnownContextFieldNames
        {
            public const string ServiceName = "ServiceFabric.ServiceName";
            public const string ServiceTypeName = "ServiceFabric.ServiceTypeName";
            public const string PartitionId = "ServiceFabric.PartitionId";
            public const string ApplicationName = "ServiceFabric.ApplicationName";
            public const string ApplicationTypeName = "ServiceFabric.ApplicationTypeName";
            public const string NodeName = "ServiceFabric.NodeName";
            public const string InstanceId = "ServiceFabric.InstanceId";
            public const string ReplicaId = "ServiceFabric.ReplicaId";
            public const string CodePackageName = "ServiceFabric.CodePackageName";
            public const string DnsName = "ServiceFabric.DnsName";
            public const string ReplicaName = "ServiceFabric.ReplicaName";
        }

        // Not all of these variables are currently read, but what we know currently are listed so it's easier to find them if we need them in the future
        private class KnownEnvironmentVariableName
        {
            public const string ServiceName = "Fabric_ServiceName";
            public const string ServicePackageName = "Fabric_ServicePackageName";
            public const string ServicePackageInstanceId = "Fabric_ServicePackageInstanceId";
            public const string ServicePackageActivatonId = "Fabric_ServicePackageActivationId";
            public const string NodeName = "Fabric_NodeName";
            public const string PartitionId = "Fabric_Id";
            public const string ApplicationName = "Fabric_ApplicationName";
            public const string CodePackageName = "Fabric_CodePackageName";
            public const string ConfigurationIdentifier = "Fabric_Epoch";
            public const string DnsName = "Fabric_ServiceDnsName";
            public const string ReplicaName = "Fabric_ReplicaName";
            public const string ReplicaId = "Fabric_ReplicaId";
        }
    }
}

