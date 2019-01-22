﻿// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using System;
using ManagedBass;
using osu.Framework.Allocation;
using osu.Framework.Platform;

namespace osu.Framework.Audio.Callbacks
{
    /// <summary>
    /// Wrapper on <see cref="SyncProcedure"/> to provide functionality in <see cref="BassCallback"/>.
    /// </summary>
    public class SyncCallback : BassCallback
    {
        public SyncProcedure Callback => RuntimeInfo.SupportsJIT ? Sync : syncCallback;

        public readonly SyncProcedure Sync;

        public SyncCallback(SyncProcedure proc)
        {
            Sync = proc;
        }

        [MonoPInvokeCallback(typeof(SyncProcedure))]
        private static void syncCallback(int handle, int channel, int data, IntPtr user)
        {
            var ptr = new ObjectHandle<SyncCallback>(user);
            ptr.Target.Sync(handle, channel, data, user);
        }
    }
}
